using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IAmenityRepository
    {
        public Task<IEnumerable<HotelAmenityDTO>> GetAllHotelAmenity();
        public Task<HotelAmenityDTO> GetHotelAmenity(int amenityId);
        public Task<HotelAmenityDTO> IsSameNameAmenityAlreadyExists(string name);
    }
}
