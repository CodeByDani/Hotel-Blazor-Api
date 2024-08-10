﻿using System;
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
        public string Details { get; set; }
        public string SqFt { get; set; }
        public double TotalDays { get; set; }
        public double TotalAmount { get; set; }

        public virtual ICollection<HotelRoomImageDTO> HotelRoomImages { get; set; }

        public List<string> ImageUrls { get; set; }
        public bool IsBooked { get; set; }
    }
}
