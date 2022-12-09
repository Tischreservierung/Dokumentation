using Microsoft.EntityFrameworkCore;
using Tischreservierung.Models;

namespace Tischreservierung.Data
{
    public class RestaurantCategoryRepository : IRestaurantCategoryRepository
    {
        private OnlineReservationContext _context;

        public RestaurantCategoryRepository(OnlineReservationContext context)
        {
            _context = context;
        }

        public void DeleteRestaurantCategory(RestaurantCategory restaurantCategory)
        {
            _context.RestaurantCategory.Remove(restaurantCategory);
        }

        public async Task<RestaurantCategory?> GetRestaurantCategory(string id)
        {
            return await _context.RestaurantCategory.FindAsync(id);
        }

        public async Task<IEnumerable<RestaurantCategory>> GetRestaurantCategories()
        {
            return await _context.RestaurantCategory.ToListAsync();
        }

        public void InsertRestaurantCategory(RestaurantCategory restaurantCategory)
        {
            _context.RestaurantCategory.Add(restaurantCategory);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
