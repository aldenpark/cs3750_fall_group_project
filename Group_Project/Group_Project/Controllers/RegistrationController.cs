using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group_Project.Data.Repository.IRepository;
using Group_Project.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Group_Project.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegistrationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<Registration>
        [HttpGet]
        public IActionResult Get()
        {

            var Courses = _unitOfWork.Course.GetAll();
            //if (CourseName != null)
            //    Courses = Courses.Where(c => c.CourseName.Contains(c.CourseName));
            //if (CourseNumber != null)
            //    Courses = Courses.Where(c => c.CourseNumber == c.CourseNumber);
            //if (Department != null)
            //    Courses = Courses.Where(c => c.Department.Contains(c.Department));
            //if (CreditHours != null)
            //    Courses = Courses.Where(c => c.CreditHours == c.CreditHours);

            return Json(new { Courses });
        }

        // GET api/<Registration>/5
        [HttpGet("{id}")]
        public string Get([FromBody] Course2 id)
        {
            return "value";
        }

        [HttpGet("{CourseName},{CourseNumber},{Department},{CreditHours}")]
        public string Get(string CourseName, string CourseNumber, string Department, string CreditHours)
        {
            return "value";
        }


        [HttpPost]
        public void Post()
        {
        }

        [HttpPost("{id}")]
        public void Post(int id, [FromBody] string value)
        {
        }

        // PUT api/<Registration>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Registration>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
