using AutoMapper;
using DataAcesss.Data;
using Models;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HotelRoom, HotelRoomDTO>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.CityHotel.Title))  // Assuming CityHotel has a Name property
                .ForMember(dest => dest.PlaceType, opt => opt.MapFrom(src => src.PlaceType.ToString()))  // Assuming PlaceType has a Name property
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.HotelRoomImages))  // Assuming HotelRoomImages has URLs
                .ForMember(dest => dest.TotalDays, opt => opt.Ignore())  // Assuming it's calculated elsewhere
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())  // Assuming it's calculated elsewhere
                .ForMember(dest => dest.Ideas, opt => opt.Ignore())  // Custom logic required
                .ForMember(dest => dest.IsBooked, opt => opt.Ignore());  // Custom logic required

            CreateMap<HotelRoom, HotelRoomDTO>();
            CreateMap<HotelRoomDTO, HotelRoom>();
            CreateMap<CityDto, City>();
            CreateMap<City, CityDto>();
            CreateMap<HotelAmenity, HotelAmenityDTO>().ReverseMap();

            CreateMap<HotelRoomImage, HotelRoomImageDTO>().ReverseMap();

            CreateMap<RoomOrderDetailsDTO, RoomOrderDetails>();

        }
    }
}
