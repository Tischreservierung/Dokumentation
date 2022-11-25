using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tischreservierung.Models
{
    public class ZipCode
    { 
        [Key]
        public int Id { get; set; }

        [Required]
        public string ZipCodeNr { get; set; } = String.Empty;

        [Required]
        public string Location { get; set; } = String.Empty;

        [Required]
        public string District { get; set; } = String.Empty; 
    }
}
