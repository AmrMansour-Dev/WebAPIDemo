using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDBContext _dbContext;
        public EmployeeController(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            List<Employee> employees = _dbContext.Employees.ToList();

            return Ok(employees);
        }

        [HttpGet("{ID:int}")]
        public IActionResult GetbyID(int ID) //ModelBinder (primitive : Route(Parameter or querystring) | Complex : request body
        {
            Employee emp = _dbContext.Employees.FirstOrDefault(e => e.ID == ID);

            return Ok(emp);
        }

        [HttpGet("{Name:alpha}")]
        public IActionResult GetbyName(string Name) //ModelBinder (primitive : Route(Parameter or querystring) | Complex : request body
        {
            Employee emp = _dbContext.Employees.FirstOrDefault(e => e.Name == Name);

            return Ok(emp);
        }

        [HttpPost]
        public IActionResult PostEmployee()
        {

            return Ok();
        }
    }
}
