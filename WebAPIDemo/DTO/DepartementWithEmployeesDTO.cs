using WebAPIDemo.Models;

namespace WebAPIDemo.DTO
{
    public class DepartementWithEmployeesDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<EmployeeDTO> Emps { get; set; } = new List<EmployeeDTO>();
    }
    public class EmployeeDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
