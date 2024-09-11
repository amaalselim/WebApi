using Assignment_1.Models;
using Assignment_1.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployee _employee;
        public EmployeesController(IEmployee employee)
        {
            _employee = employee;
        }
        [HttpGet]
        public IActionResult GetEmployee()
        {
            var emp = _employee.GetAll();
            return Ok(emp);
        }
        [HttpGet("{id:int}",Name ="FindEmpByIdRoute")]
        public IActionResult GetEmpById(int id)
        {
            var emp= _employee.GetById(id);
            return Ok(emp);
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employee.Insert(employee);
                string url = Url.Link("FindEmpByIdRoute", new { id = employee.Id });
                return Created(url, employee);

            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute]int id,[FromBody]Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee emp = _employee.GetById(id);
                _employee.Update(id,emp);
                return StatusCode(StatusCodes.Status204NoContent, "Data Saved");
            }
            return BadRequest("Data Not Valid");
        }
        [HttpDelete("{id:int}")]
        public IActionResult RemoveEmployee(int id)
        {
            Employee emp = _employee.GetById(id);
            _employee.Delete(id);
            return StatusCode(StatusCodes.Status204NoContent, "Data Saved");

        }
    }
}
