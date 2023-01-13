using Tischreservierung.Models;

namespace Tischreservierung.Data.RestaurantRepo
{
    public interface IOpeningTimeRepository
    {
        Task<IEnumerable<RestaurantOpeningTime>> GetOpeningTimesByRestaurant(int id);
        Task<IEnumerable<RestaurantOpeningTime>> GetOpeningTimesByDay(int day);
        Task<IEnumerable<RestaurantOpeningTime>> GetOpeningTimesByDayAndRestaurant(int id, int day);

        Task<RestaurantOpeningTime?> GetOpeningTime(int id);

        void DeleteOpeningTime(RestaurantOpeningTime openingTime);
        void UpdateOpeningTime(RestaurantOpeningTime openingTime);
        void InsertRestaurantOpeningTime(RestaurantOpeningTime openingTime);
        Task Save();
    }
}
