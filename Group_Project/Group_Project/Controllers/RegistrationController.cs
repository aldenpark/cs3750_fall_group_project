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
                Courses = Courses.Where(c => c.CourseName.ToLower().Contains(filter.CourseName.ToLower()));
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

            var CourseList = Courses.ToList();
            if (userId > 0)
            {
                int i = 0;
                var RegisteredCourses = _unitOfWork.Registration.GetAll(r => r.StudentID == userId);
                if(RegisteredCourses.Count() > 0)
                {
                    foreach (var reg in RegisteredCourses)
                    {
                        i = 0;
                        foreach (var crs in CourseList)
                        {
                            if (crs.ID == reg.CourseID)
                                CourseList[i].Registered = true;
                            i++;
                        }
                    }
                }
            }

            return Json(new { CourseList });
        }

        [HttpGet]
        public void Get()
        {
        }

        // PUT api/<Registration>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] bool value)
        {
            int userId = 0;
            if (HttpContext.Session.GetInt32(SD.UserSessionId) != null)
            {
                if (HttpContext.Session.GetInt32(SD.UserSessionId).HasValue)
                {
                    userId = HttpContext.Session.GetInt32(SD.UserSessionId).Value;
                }
            }

            if(userId > 0)
            {
                if(value == true) {
                    var RegistrationObj = new Registration();
                    RegistrationObj.CourseID = id;
                    RegistrationObj.StudentID = userId;
                    _unitOfWork.Registration.Add(RegistrationObj);
                }
                else
                {
                    var objFromDb = _unitOfWork.Registration.GetFirstorDefault(r => r.CourseID == id && r.StudentID == userId);
                    _unitOfWork.Registration.Remove(objFromDb);
                }
                _unitOfWork.Save();
            }
        }

    }
}
