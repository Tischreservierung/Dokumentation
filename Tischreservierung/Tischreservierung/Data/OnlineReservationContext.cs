using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using Tischreservierung.Models.Person;
﻿using System;
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

        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
        public DbSet<RestaurantCategory> RestaurantCategory => Set<RestaurantCategory>();
        public DbSet<RestaurantTable> RestaurantTables => Set<RestaurantTable>();
        public DbSet<RestaurantOpeningTime> RestaurantOpeningTimes => Set<RestaurantOpeningTime>();
        public DbSet<ZipCode> Zipcodes => Set<ZipCode>();
    }
}
