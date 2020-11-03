using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group_Project.Data.Repository.IRepository;
using Group_Project.Models;
using Group_Project.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;

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
        public IActionResult Post([FromBody] SearchCourses filter) //[FromBody]
        {
            var Courses = _unitOfWork.Course.GetAll();
            if (filter.CourseName != "")
                Courses = Courses.Where(c => c.CourseName.Contains(filter.CourseName));
            if (filter.CourseNumber != "")
            {
                int CourseNumber = -1;
                Int32.TryParse(filter.CourseNumber, out CourseNumber);
                if (CourseNumber > -1)
                    Courses = Courses.Where(c => c.CourseNumber == CourseNumber);
            }
            if (filter.Department != "")
                Courses = Courses.Where(c => c.Department.Contains(filter.Department));
            if (filter.CreditHours != "")
            {
                int CreditHours = -1;
                Int32.TryParse(filter.CreditHours, out CreditHours);
                if (CreditHours > -1)
                    Courses = Courses.Where(c => c.CreditHours == CreditHours);
            }

            int userId = 0;
            if (HttpContext.Session.GetInt32(SD.UserSessionId) != null)
            {
                if (HttpContext.Session.GetInt32(SD.UserSessionId).HasValue)
                {
                    userId = HttpContext.Session.GetInt32(SD.UserSessionId).Value;
                }
            }

            if (userId > 0)
            {
                var RegisteredCourses = _unitOfWork.Registration.GetAll(r => r.StudentID == userId);
                if(RegisteredCourses.Count() > 0)
                {
                    foreach (var crs in RegisteredCourses)
                    {
                        var reg = Courses.FirstOrDefault(c => c.ID == crs.ID);
                        reg.Registered = true;
                    }
                }
            }

            return Json(new { Courses });
        }

        [HttpGet]
        public void Get()
        {
        }

        // PUT api/<Registration>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] bool value)
        {
            var test = value;
        }

        // DELETE api/<Registration>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
