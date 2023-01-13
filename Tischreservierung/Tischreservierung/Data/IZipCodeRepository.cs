using Tischreservierung.Models;

namespace Tischreservierung.Data
{
    public interface IZipCodeRepository
    {
        Task<List<ZipCode>> GetZipCodes();
        Task<List<ZipCode>> GetByLocation(string location);

        Task<List<ZipCode>> GetByZipCode(string zipcode);
      

        Task<List<ZipCode>> GetByDistrict(string district);

        Task Save();

    }
}
