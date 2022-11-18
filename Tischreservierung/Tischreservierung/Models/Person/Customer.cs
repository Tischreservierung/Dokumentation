using System.ComponentModel.DataAnnotations.Schema;

namespace Tischreservierung.Models.Person
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerNumber { get; set; } = string.Empty;

        [ForeignKey(nameof(Person_Id))]
        public int Person_Id { get; set; }
        public Person? Person { get; set; }
    }
}
