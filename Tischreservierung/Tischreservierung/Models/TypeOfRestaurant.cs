using System.ComponentModel.DataAnnotations;

namespace Tischreservierung.Models
{
    public class TypeOfRestaurant
    {
        [Key]
        public string RestaurantType { get; set; } = string.Empty;
        public List<Restaurant>? Restaurants { get; set; }
    }
}
