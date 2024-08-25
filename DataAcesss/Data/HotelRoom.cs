using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAcesss.Data
{
    public class HotelRoom
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Occupancy { get; set; }
        [Required]
        public double RegularRate { get; set; }
        public string Details { get; set; }
        public string SqFt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public City CityHotel { get; set; }
        public int CityHotelId { get; set; }
        public PlaceType PlaceType { get; set; }
        public virtual ICollection<HotelRoomImage> HotelRoomImages { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }
        public virtual ICollection<HotelRoomHotelAmenity> HotelRoomHotelAmenity { get; set; }
    }
}
