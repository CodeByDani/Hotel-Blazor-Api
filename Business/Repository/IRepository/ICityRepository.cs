using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface ICityRepository
    {
        public Task<List<CityDto>> GetCities();
        public Task<string> GetCityName(int id);

    }
}