using System.ComponentModel.DataAnnotations;

namespace Tischreservierung.Models
{
    public class RestaurantCategory
    {
        [Key]
        public string Category { get; set; } = string.Empty;
        public List<Restaurant>? Restaurants { get; set; }
    }
}
