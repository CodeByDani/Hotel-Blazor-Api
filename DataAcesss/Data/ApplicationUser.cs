using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DataAcesss.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public virtual ICollection<HotelRoom> HotelRooms { get; set; }
        public virtual ICollection<RoomOrderDetails> OrderDetailsCollection { get; set; }
    }
}
