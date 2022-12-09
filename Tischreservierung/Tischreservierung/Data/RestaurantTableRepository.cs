using Microsoft.EntityFrameworkCore;
using Tischreservierung.Models;

namespace Tischreservierung.Data
{
    public class RestaurantTableRepository : IRestaurantTableRepository
    {
        private OnlineReservationContext _context;

        public RestaurantTableRepository(OnlineReservationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RestaurantTable>> GetRestaurantTables()
        {
            return await _context.RestaurantTables.ToListAsync();
        }

        public async Task<RestaurantTable?> GetRestaurantTableById(int id)
        {
            return await _context.RestaurantTables.FindAsync(id);
        }

        public async Task<IEnumerable<RestaurantTable>> GetRestaurantTablesByRestaurant(int restaurantId)
        {
            return await _context.RestaurantTables.Where(r => r.RestaurantId == restaurantId).ToListAsync();
        }

        public void InsertRestaurantTable(RestaurantTable restaurantTable)
        {
            _context.RestaurantTables.Add(restaurantTable);
        }

        public void UpdateRestaurantTable(RestaurantTable restaurantTable)
        {
            _context.RestaurantTables.Update(restaurantTable);
        }

        public void DeleteRestaurantTable(RestaurantTable restaurantTable)
        {
            _context.RestaurantTables.Remove(restaurantTable);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
