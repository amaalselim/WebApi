using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //api/Department "Restful"
    //Resource Model (Department)

    public class DepartmentController : ControllerBase
    {
        private readonly ITIEntity _context;

        public DepartmentController(ITIEntity context)
        {
            _context = context; 
        }

        //api/department verb Get
        [HttpGet]
        public IActionResult GetDepartment()
        {
            List<Department> dept = _context.departments.ToList();
            return Ok(dept);
        }
        /*[HttpGet]
        [Route("{id:int}")]//api/department/{id}*/
        [HttpGet("{id:int}",Name="FindDeptByIdRoute")]   //api/department/{id}
        public IActionResult GetDepartment(int id) //Routedata=>primptive
        {
            Department dept = _context.departments.FirstOrDefault(x=>x.Id==id);
            return Ok(dept);
        }
        [HttpGet]
        [Route("{name:alpha}")]//api/department/{name}
        public IActionResult GetDepartmentByName(string name) //Routedata=>primptive
        {
            Department dept = _context.departments.FirstOrDefault(x => x.Name==name);
            if (dept == null)
            {
                return BadRequest("Name Not Found");
            }
            return Ok(dept);
        }

        //api/department verb Post
        [HttpPost] //add New Resource
        public IActionResult Add(Department dept)
        {
            if(ModelState.IsValid)
            {
                _context.departments.Add(dept);
                _context.SaveChanges();
                //string url = "http://localhost:35769/api/Department/" + dept.Id;
                string url = Url.Link("FindDeptByIdRoute", new { id = dept.Id });
                return Created(url,dept);// "Dept Add Success"
            }
            return BadRequest(ModelState);
        }

        //api/department verb Put
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute]int id,[FromBody]Department newDept)
        {
            if (ModelState.IsValid)
            {
                Department oldDept = _context.departments.FirstOrDefault(x => x.Id == id);
                oldDept.Name=newDept.Name;
                oldDept.Manager= newDept.Manager;
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status204NoContent, "Data Saved");
            }
            return BadRequest("Data Not Valid");
        }

        //api/department verb Delete
        [HttpDelete("{id:int}")]
        public IActionResult RemoveDepartment(int id)
        {
            Department oldDept = _context.departments.FirstOrDefault(x => x.Id == id);
            _context.departments.Remove(oldDept);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent, "Data Saved");
        }
    }
}
