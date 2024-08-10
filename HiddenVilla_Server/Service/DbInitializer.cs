using Common;
using DataAcesss.Data;
using HiddenVilla_Server.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HiddenVilla_Server.Service
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initalize()
        {
            AddCities();
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            if (_db.Roles.Any(x => x.Name == SD.Role_Admin)) return;

            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            }, "Admin123*").GetAwaiter().GetResult();

            IdentityUser user = _db.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
            _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
        }

        private void AddCities()
        {
            var cities = new List<City>
{
            new City { Title = "تهران", Code = "TEH" },
            new City { Title = "مشهد", Code = "MHD" },
            new City { Title = "اصفهان", Code = "ISF" },
            new City { Title = "شیراز", Code = "SHZ" },
            new City { Title = "تبریز", Code = "TBZ" },
            new City { Title = "اهواز", Code = "AHW" },
            new City { Title = "کرج", Code = "KRJ" },
            new City { Title = "قم", Code = "QOM" },
            new City { Title = "کرمانشاه", Code = "KRM" },
            new City { Title = "زنجان", Code = "ZAN" },
            new City { Title = "بندرعباس", Code = "BDH" },
            new City { Title = "کیش", Code = "KSH" },
            new City { Title = "رشت", Code = "RAS" },
            new City { Title = "بروجرد", Code = "BRJ" },
            new City { Title = "ساری", Code = "SAR" },
            new City { Title = "نوشهر", Code = "NVS" },
            new City { Title = "یاسوج", Code = "YAS" },
            new City { Title = "خرمشهر", Code = "KHM" },
            new City { Title = "سنندج", Code = "SND" },
            new City { Title = "ایلام", Code = "ILM" },
            new City { Title = "همدان", Code = "HMD" },
            new City { Title = "ارومیه", Code = "URM" },
            new City { Title = "لار", Code = "LAR" },
            new City { Title = "خوی", Code = "KHO" },
            new City { Title = "بهبهان", Code = "BHB" },
            new City { Title = "تربت حیدریه", Code = "TRH" },
            new City { Title = "سمنان", Code = "SMN" },
            new City { Title = "فیروزآباد", Code = "FIR" },
            new City { Title = "آبادان", Code = "ABD" },
            new City { Title = "طهران", Code = "TEH" },
            new City { Title = "کاشان", Code = "KSH" },
            new City { Title = "بابل", Code = "BAB" },
            new City { Title = "قزوین", Code = "QZN" },
            new City { Title = "شاهرود", Code = "SHD" },
            new City { Title = "اردبیل", Code = "ARD" },
            new City { Title = "شهرکرد", Code = "SHK" },
            new City { Title = "دامغان", Code = "DAM" },
            new City { Title = "قائنات", Code = "QAI" },
            new City { Title = "زرند", Code = "ZRN" },
            new City { Title = "ایوانکی", Code = "IVK" },
};

            if (!_db.Cities.Any())
            {
                _db.Cities.AddRange(cities);
                _db.SaveChanges();
            }
        }
    }
}
