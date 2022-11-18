using System.ComponentModel.DataAnnotations.Schema;

namespace Tischreservierung.Models.Person
{
    public class Employee
    {
        public int Id { get; set; }

        public bool isAdmin { get; set; }

        [ForeignKey(nameof(Person_Id))]
        public int Person_Id { get; set; }
        public Person? Person { get; set; }

        //Referenz Restaurant
    }
}
