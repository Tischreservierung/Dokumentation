using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tischreservierung.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public ZipCode? ZipCode { get; set; }
        public string Place { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;

        public List<RestaurantCategory>? RestaurantTypes { get; set; }
        public List<RestaurantTable>? Tables { get; set; }
        public List<RestaurantOpeningTime>? OpeningTimes { get; set; }
    }
}
