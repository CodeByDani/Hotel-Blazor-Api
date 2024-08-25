using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAcesss.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<HotelRoomHotelAmenity> HotelRoomHotelAmenities { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<HotelRoomImage> HotelRoomImages { get; set; }
        public DbSet<HotelAmenity> HotelAmenities { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<RoomOrderDetails> RoomOrderDetails { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
