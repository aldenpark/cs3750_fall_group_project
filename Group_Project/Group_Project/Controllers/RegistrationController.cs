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
        [HttpPost]
        public IActionResult Post([FromForm] SearchCourses filter) //[FromBody]
        {
            var test = filter;
            var Courses = _unitOfWork.Course.GetAll();
            if (filter.CourseName != null)
                Courses = Courses.Where(c => c.CourseName.Contains(filter.CourseName));
            //if (CourseNumber != null)
            //    Courses = Courses.Where(c => c.CourseNumber == c.CourseNumber);
            //if (Department != null)
            //    Courses = Courses.Where(c => c.Department.Contains(c.Department));
            //if (CreditHours != null)
            //    Courses = Courses.Where(c => c.CreditHours == c.CreditHours);

            return Json(new { filter });
        }

        [HttpGet]
        public IActionResult Get([FromForm] SearchCourses filter)
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

        [HttpGet("{CourseName},{CourseNumber},{Department},{CreditHours}")]
        public string Get(string CourseName, string CourseNumber, string Department, string CreditHours)
        {
            return "value";
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
