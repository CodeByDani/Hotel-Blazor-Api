using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiddenVilla_Server.Service.IService
{
    public interface ICityService
    {
        public Task<List<CityDto>> GetCities();
        public Task<string> GetCityName(int id);

    }
}