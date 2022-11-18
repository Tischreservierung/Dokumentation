using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tischreservierung.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;
        public List<TypeOfRestaurant>? RestaurantTypes { get; set; }
    }
}
