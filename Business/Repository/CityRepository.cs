using AutoMapper;
using Business.Repository.IRepository;
using DataAcesss.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public CityRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }
        public async Task<List<CityDto>> GetCities()
        {
            var cities = await _db.Cities.ToListAsync();
            var res = _mapper.Map<List<CityDto>>(cities);
            return res;
        }

        public async Task<string> GetCityName(int id)
        {
            var res = await _db.Cities.FirstOrDefaultAsync(p => p.Id == id);
            return res.Title;
        }
    }
}