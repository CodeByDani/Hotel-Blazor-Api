using Business.Repository.IRepository;
using HiddenVilla_Server.Service.IService;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiddenVilla_Server.Service
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<List<CityDto>> GetCities()
        {
            var res = await _cityRepository.GetCities();
            return res;
        }

        public async Task<string> GetCityName(int id)
        {
            return await _cityRepository.GetCityName(id);
        }
    }
}