using Tischreservierung.Models;

namespace Tischreservierung.Data
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetRestaurants();
        Task<Restaurant?> GetRestaurantById(int id);
        void InsertRestaurant(Restaurant restaurant);
        void DeleteRestaurant(Restaurant restaurant);
        void UpdateRestaurant(Restaurant restaurant);
        Task Save();
    }
}
