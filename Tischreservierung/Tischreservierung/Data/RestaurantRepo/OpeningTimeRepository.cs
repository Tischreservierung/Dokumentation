using Microsoft.EntityFrameworkCore;
using Tischreservierung.Models;

namespace Tischreservierung.Data.RestaurantRepo
{
    public class OpeningTimeRepository : IOpeningTimeRepository
    {
        private OnlineReservationContext _context;

        public OpeningTimeRepository(OnlineReservationContext context)
        {
            _context = context;
        }

        public void DeleteOpeningTime(RestaurantOpeningTime openingTime)
        {
            _context.RestaurantOpeningTimes.Remove(openingTime);
        }

        public async Task<RestaurantOpeningTime?> GetOpeningTime(int id)
        {
            return await _context.RestaurantOpeningTimes.FindAsync(id);
        }

        public async Task<IEnumerable<RestaurantOpeningTime>> GetOpeningTimesByDay(int day)
        {
            return await _context.RestaurantOpeningTimes.Where(oT => oT.Day == day).ToListAsync();
        }

        public async Task<IEnumerable<RestaurantOpeningTime>> GetOpeningTimesByDayAndRestaurant(int id, int day)
        {
            return await _context.RestaurantOpeningTimes.Where(oT => oT.Day == day && oT.RestaurantId == id).ToListAsync();
        }


        public async Task<IEnumerable<RestaurantOpeningTime>> GetOpeningTimesByRestaurant(int id)
        {
            return await _context.RestaurantOpeningTimes.Where(oT => oT.RestaurantId == id).ToListAsync();
        }

        public void InsertRestaurantOpeningTime(RestaurantOpeningTime openingTime)
        {
            _context.RestaurantOpeningTimes.Add(openingTime);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateOpeningTime(RestaurantOpeningTime openingTime)
        {
            _context.RestaurantOpeningTimes.Update(openingTime);
        }
    }
}
