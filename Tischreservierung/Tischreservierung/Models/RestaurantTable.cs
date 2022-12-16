using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tischreservierung.Models
{
    public class RestaurantTable
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue)]
        public int SeatPlaces { get; set; }
        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
    }
}
