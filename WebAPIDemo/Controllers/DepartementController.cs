using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIDemo.DTO;
using WebAPIDemo.Migrations;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartementController : ControllerBase
    {
        private readonly AppDBContext _dbContext;
        public DepartementController(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpGet("{ID:int}")]
        public IActionResult GetDept(int ID)
        {
            Departement dept = _dbContext.Departement.Include(d => d.Employees).FirstOrDefault();
            //map model to dto
            DepartementWithEmployeesDTO deptdto = new DepartementWithEmployeesDTO();
            deptdto.ID = dept.ID;
            deptdto.Name = dept.Name;
            foreach(var item in dept.Employees)
            {
                deptdto.Emps.Add(new EmployeeDTO() { ID = item.ID, Name = item.Name });
            }
            return Ok(deptdto);
        }
    }
}
