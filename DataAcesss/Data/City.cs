using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAcesss.Data
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public virtual ICollection<HotelRoom> HotelRooms { get; set; }
    }
}