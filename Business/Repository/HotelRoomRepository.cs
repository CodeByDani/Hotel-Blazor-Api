using AutoMapper;
using Business.Repository.IRepository;
using DataAcesss.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom hotelRoom = new HotelRoom()
            {
                Name = hotelRoomDTO.Name,
                CityHotelId = hotelRoomDTO.CityHotelId,
                Details = hotelRoomDTO.Details,
                UserId = hotelRoomDTO.UserId,
                Occupancy = hotelRoomDTO.Occupancy,
                SqFt = hotelRoomDTO.SqFt,
                RegularRate = hotelRoomDTO.RegularRate,
                CreatedDate = DateTime.Now,
                CreatedBy = "",
                PlaceType = SetPlaceType(hotelRoomDTO.PlaceType),
            };
            var addedHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
            await _db.SaveChangesAsync();
            await AddAmenitiesToHotelRoomAsync(addedHotelRoom.Entity.Id, hotelRoomDTO.Ideas);
            return _mapper.Map<HotelRoom, HotelRoomDTO>(addedHotelRoom.Entity);
        }

        public async Task<int> DeleteHotelRoom(int roomId)
        {
            var roomDetails = await _db.HotelRooms.FindAsync(roomId);
            if (roomDetails != null)
            {

                var allimages = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();

                _db.HotelRoomImages.RemoveRange(allimages);
                _db.HotelRooms.Remove(roomDetails);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms(string userId, string checkInDateStr, string checkOutDatestr)
        {
            try
            {
                IEnumerable<HotelRoomDTO> hotelRoomDTOs =
                            _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>
                            (_db.HotelRooms.Include(x => x.HotelRoomImages)
                                .Include(x => x.CityHotel)
                                .Where(p => p.UserId == userId));

                if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDatestr))
                {
                    foreach (HotelRoomDTO hotelRoom in hotelRoomDTOs)
                    {
                        hotelRoom.IsBooked = await IsRoomBooked(hotelRoom.Id, checkInDateStr, checkOutDatestr);
                    }
                }
                return hotelRoomDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkInDateStr, string checkOutDatestr)
        {
            try
            {
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                    await _db.HotelRooms.Include(x => x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId));

                if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDatestr))
                {
                    hotelRoom.IsBooked = await IsRoomBooked(roomId, checkInDateStr, checkOutDatestr);
                }

                return hotelRoom;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<bool> IsRoomBooked(int RoomId, string checkInDatestr, string checkOutDatestr)
        {
            try
            {
                if (!string.IsNullOrEmpty(checkOutDatestr) && !string.IsNullOrEmpty(checkInDatestr))
                {
                    DateTime checkInDate = DateTime.ParseExact(checkInDatestr, "MM/dd/yyyy", null);
                    DateTime checkOutDate = DateTime.ParseExact(checkOutDatestr, "MM/dd/yyyy", null);

                    var existingBooking = await _db.RoomOrderDetails.Where(x => x.RoomId == RoomId && x.IsPaymentSuccessful &&
                       //check if checkin date that user wants does not fall in between any dates for room that is booked
                       ((checkInDate < x.CheckOutDate && checkInDate.Date >= x.CheckInDate)
                       //check if checkout date that user wants does not fall in between any dates for room that is booked
                       || (checkOutDate.Date > x.CheckInDate.Date && checkInDate.Date <= x.CheckInDate.Date)
                       )).FirstOrDefaultAsync();

                    if (existingBooking != null)
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        //if unique returns null else returns the room obj
        public async Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0)
        {
            try
            {
                if (roomId == 0)
                {
                    HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                        await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));

                    return hotelRoom;
                }
                else
                {
                    HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                        await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()
                        && x.Id != roomId));

                    return hotelRoom;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if (roomId == hotelRoomDTO.Id)
                {
                    //valid
                    var amenities = _db.HotelRoomHotelAmenities
                        .Where(hh => hh.HotelRoomId == roomId)
                        .Select(hh => hh.HotelAmenity)
                        .ToList();
                    _db.HotelAmenities.RemoveRange(amenities);
                    HotelRoom room = await _db.HotelRooms.FindAsync(roomId);
                    room.Name = hotelRoomDTO.Name;
                    room.CityHotelId = hotelRoomDTO.CityHotelId;
                    room.Details = hotelRoomDTO.Details;
                    room.UserId = hotelRoomDTO.UserId;
                    room.Occupancy = hotelRoomDTO.Occupancy;
                    room.SqFt = hotelRoomDTO.SqFt;
                    room.RegularRate = hotelRoomDTO.RegularRate;
                    room.PlaceType = SetPlaceType(hotelRoomDTO.PlaceType);
                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.Now;
                    var updatedRoom = _db.HotelRooms.Update(room);
                    await _db.SaveChangesAsync();
                    await AddAmenitiesToHotelRoomAsync(roomId, hotelRoomDTO.Ideas);
                    return _mapper.Map<HotelRoom, HotelRoomDTO>(updatedRoom.Entity);
                }
                else
                {
                    //invalid
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private PlaceType SetPlaceType(string type)
        {
            return type switch
            {
                "هتل" => PlaceType.Hotel,
                "آپارتمان" => PlaceType.Apartment,
                "ویلا" => PlaceType.Villa,
                "باغ" => PlaceType.Garden,
                "Hotel" => PlaceType.Hotel,
                "Apartment" => PlaceType.Apartment,
                "Villa" => PlaceType.Villa,
                "Garden" => PlaceType.Garden,
            };
        }

        private async Task AddAmenitiesToHotelRoomAsync(int roomId, List<int> amenityIds)
        {
            // Retrieve the hotel room with existing amenities
            var hotelRoom = await _db.HotelRooms
                .Include(r => r.HotelRoomHotelAmenity)
                .ThenInclude(rha => rha.HotelAmenity)
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (hotelRoom == null)
            {
                throw new Exception("Hotel room not found.");
            }

            // Retrieve the amenities to add
            var amenitiesToAdd = await _db.HotelAmenities
                .Where(a => amenityIds.Contains(a.Id))
                .ToListAsync();

            // Add new amenities to the hotel room
            foreach (var amenity in amenitiesToAdd)
            {
                if (!hotelRoom.HotelRoomHotelAmenity.Any(rha => rha.HotelAmenityId == amenity.Id))
                {
                    hotelRoom.HotelRoomHotelAmenity.Add(new HotelRoomHotelAmenity
                    {
                        HotelRoomId = hotelRoom.Id,
                        HotelAmenityId = amenity.Id
                    });
                }
            }

            // Save changes to the database
            await _db.SaveChangesAsync();
        }

    }
}
