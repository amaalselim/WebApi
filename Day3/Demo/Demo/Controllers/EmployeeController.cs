using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo.DTO;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        ITIEntity entity = new ITIEntity();
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Employee employeeModel = entity.employees.Include(e => e.Department).FirstOrDefault(x => x.Id == id);
            EmployeeWithDepartmentNameDTO empDTO= new EmployeeWithDepartmentNameDTO();
            empDTO.EmployeeName = employeeModel.Name;
            empDTO.DepartmentName = employeeModel.Department.Name;
            empDTO.Id=employeeModel.Id;
            return Ok(empDTO);    
        }
    }
}
