using Microsoft.EntityFrameworkCore;
using Tischreservierung.Models;

namespace Tischreservierung.Data.RestaurantRepo
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private OnlineReservationContext _context;

        public RestaurantRepository(OnlineReservationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurants()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant?> GetRestaurantById(int id)
        {
            return await _context.Restaurants.FindAsync(id);
        }
        public void InsertRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
        }

        public void DeleteRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
