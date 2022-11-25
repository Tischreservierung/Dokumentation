using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tischreservierung.Models;

namespace Tischreservierung.Data
{
    public class OnlineReservationContext : DbContext
    {
        public OnlineReservationContext(DbContextOptions<OnlineReservationContext> options)
            : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
        public DbSet<TypeOfRestaurant> TypeOfRestaurants => Set<TypeOfRestaurant>();
        public DbSet<RestaurantTable> RestaurantTables => Set<RestaurantTable>();
    }
}
