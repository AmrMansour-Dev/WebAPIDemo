using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIDemo.Models
{
    public class Employee
    {
        public int ID { get; set; }

        [MinLength(3)]
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }

        [Range(1000,10000)]
        public int Salary { get; set; }
        public int Age { get; set; }

        [ForeignKey("Departement")]
        public int? Dept_ID { get; set; }

        public Departement? Departement { get; set; }

    }
}
