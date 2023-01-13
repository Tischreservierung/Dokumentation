using Tischreservierung.Models;

namespace Tischreservierung.Data.RestaurantRepo
{
    public interface IRestaurantTableRepository
    {
        Task<IEnumerable<RestaurantTable>> GetRestaurantTables();
        Task<RestaurantTable?> GetRestaurantTableById(int id);
        Task<IEnumerable<RestaurantTable>> GetRestaurantTablesByRestaurant(int restaurantId);
        void InsertRestaurantTable(RestaurantTable restaurantTable);
        void DeleteRestaurantTable(RestaurantTable restaurantTable);
        void UpdateRestaurantTable(RestaurantTable restaurantTable);
        Task Save();
    }
}
