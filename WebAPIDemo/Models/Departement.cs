using System.Text.Json.Serialization;

namespace WebAPIDemo.Models
{
    public class Departement
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public List<Employee>? Employees { get; set; }

    }
}
