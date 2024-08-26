using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IHotelRoomRepository
    {
        public Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkInDate = null, string checkOutDate = null);
        public Task<HotelRoomDTO> NewGetHotelRoom(int roomId, string checkInDate = null, string checkOutDate = null);
        public Task<int> DeleteHotelRoom(int roomId);
        public Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms(string userId, string checkInDate = null, string checkOutDate = null);
        public Task<IEnumerable<HotelRoomDTO>> GetAllHotelRoomsMain(string type, int cityId, string checkInDate = null, string checkOutDate = null);
        public Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0);
        public Task<bool> IsRoomBooked(int RoomId, string checkInDate, string checkOutDate);
    }
}
