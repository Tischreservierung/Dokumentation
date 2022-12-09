using Tischreservierung.Models;

namespace Tischreservierung.Data
{
    public interface IRestaurantCategoryRepository
    {
        /// <summary>
        /// Gets all Categories with restaurant
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<RestaurantCategory>> GetRestaurantCategories();
        /// <summary>
        /// Get one category with a list of all restaurants with the category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RestaurantCategory?> GetRestaurantCategory(string id);
        /// <summary>
        /// Insert a new restaurantCategory
        /// </summary>
        /// <param name="restaurantCategory"></param>
        void InsertRestaurantCategory(RestaurantCategory restaurantCategory);
        /// <summary>
        /// Delete one restaurantCategory
        /// </summary>
        /// <param name="restaurantCategory"></param>
        void DeleteRestaurantCategory(RestaurantCategory restaurantCategory);
        Task Save();
    }
}
