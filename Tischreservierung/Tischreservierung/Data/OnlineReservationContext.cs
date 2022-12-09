using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using Tischreservierung.Models.Person;

namespace Tischreservierung.Data
{
    public class OnlineReservationContext : DbContext
    {
        public OnlineReservationContext(DbContextOptions<OnlineReservationContext> options)
            : base(options)
        {

        }

        public DbSet<Customer> Customers => Set<Customer>();
    }
}
