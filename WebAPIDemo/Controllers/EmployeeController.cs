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

        [HttpPut("{ID}")]
        public IActionResult PutEmployee([FromRoute] int ID, [FromBody] Employee emp)
        {
            if (ModelState.IsValid)
            {
                Employee oldemp = _dbContext.Employees.FirstOrDefault(e => e.ID == ID);

                if (oldemp != null)
                {
                    oldemp.Name = emp.Name;
                    oldemp.Address = emp.Address;
                    oldemp.Salary = emp.Salary;
                    oldemp.Age = emp.Age;
                    _dbContext.SaveChanges();
                    return StatusCode(StatusCodes.Status204NoContent);
                }

            }

            return BadRequest();
        }

        [HttpDelete("{ID}")]
        public IActionResult DeleteEmployee([FromRoute] int ID)
        {
            Employee emp = _dbContext.Employees.FirstOrDefault(e=>e.ID == ID);

            try
            {
                _dbContext.Employees.Remove(emp);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex);
            }
        }
    }
}
