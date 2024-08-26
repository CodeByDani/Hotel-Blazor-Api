using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class HotelRoomDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "اسم اتاق الزامیست")]
        public string Name { get; set; }
        [Required(ErrorMessage = "ظرفیت الزامیست")]
        public int Occupancy { get; set; }
        [Range(1000, 3000, ErrorMessage = "بین 1000 تا 3000 تومان باید باشد")]
        public double RegularRate { get; set; }
        [Required(ErrorMessage = "حتما باید شهر را انتخاب کنید")]
        public int CityHotelId { get; set; }
        public string CityName { get; set; }
        public string Details { get; set; }
        public string SqFt { get; set; }
        public double TotalDays { get; set; }
        public double TotalAmount { get; set; }
        public string PlaceType { get; set; }
        public List<int> Ideas { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<HotelRoomImageDTO> HotelRoomImages { get; set; }
        public List<HotelAmenityDTO> HotelAmenities { get; set; }


        public List<string> ImageUrls { get; set; }
        public bool IsBooked { get; set; }
    }
}
