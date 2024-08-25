namespace DataAcesss.Data
{

    public class HotelRoomHotelAmenity
    {
        public int Id { get; set; }
        public int HotelRoomId { get; set; }
        public virtual HotelRoom HotelRoom { get; set; }
        public int HotelAmenityId { get; set; }
        public virtual HotelAmenity HotelAmenity { get; set; }

    }
}