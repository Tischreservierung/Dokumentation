using System.ComponentModel.DataAnnotations.Schema;

namespace Tischreservierung.Models.Person
{
    public class Employee : Person
    {
        public bool IsAdmin { get; set; }

        //Referenz Restaurant
    }
}
