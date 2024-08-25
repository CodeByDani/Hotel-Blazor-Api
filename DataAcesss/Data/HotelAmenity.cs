using System.Collections.Generic;

namespace DataAcesss.Data
{
    public class HotelAmenity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconStyle { get; set; }
        public virtual ICollection<HotelRoomHotelAmenity> HotelAmenities { get; set; }

    }
}
