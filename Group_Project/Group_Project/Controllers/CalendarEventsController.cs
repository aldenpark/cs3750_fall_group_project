using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Group_Project.Data;
using Group_Project.Data.Repository.IRepository;
using Group_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/*  https://github.com/edlynvillegas/evo-calendar
    https://edlynvillegas.github.io/evo-calendar/ */
namespace Group_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CalendarEventsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Calender
        [HttpGet]
        public IActionResult Index()
        {
            DateTime start_date = new DateTime(2020, 08, 24);
            DateTime end_date = new DateTime(2020, 12, 11);

            var Events = new List<Calendar>();
            var Course = _unitOfWork.Course.GetAll(); // .Where(c => c. )  loading all courses until we have a relationship setup
            foreach(var row in Course)
            {

                int count = 1;
                for (DateTime date = start_date; date <= end_date; date = date.AddDays(1))
                {
                    if (date.DayOfWeek == DayOfWeek.Sunday && row.Sunday == true)
                    {
                        Events.Add(addEvent(count, date, row));
                        count++;
                    }
                    if (date.DayOfWeek == DayOfWeek.Monday && row.Monday == true)
                    {
                        Events.Add(addEvent(count, date, row));
                        count++;
                    }
                    if (date.DayOfWeek == DayOfWeek.Tuesday && row.Tuesday == true)
                    {
                        Events.Add(addEvent(count, date, row));
                        count++;
                    }
                    if (date.DayOfWeek == DayOfWeek.Wednesday && row.Wednesday == true)
                    {
                        Events.Add(addEvent(count, date, row));
                        count++;
                    }
                    if (date.DayOfWeek == DayOfWeek.Thursday && row.Thursday == true)
                    {
                        Events.Add(addEvent(count, date, row));
                        count++;
                    }
                    if (date.DayOfWeek == DayOfWeek.Friday && row.Friday == true)
                    {
                        Events.Add(addEvent(count, date, row));
                        count++;
                    }
                    if (date.DayOfWeek == DayOfWeek.Saturday && row.Saturday == true)
                    {
                        Events.Add(addEvent(count, date, row));
                        count++;
                    }
                }

            }

            Events.Add(new Calendar()
            {
                id = "required-id-1",
                name = "New Year",
                date = "Wed Jan 01 2020 00:00:00 GMT-0800 (Pacific Standard Time)",
                type = "holiday",
                everyYear = true
            });
            Events.Add(new Calendar()
            {
                id = "required-id-2",
                name = "Valentine's Day",
                date = "Fri Feb 14 2020 00:00:00 GMT-0800 (Pacific Standard Time)",
                type = "holiday",
                everyYear = true,
                color = "#222"
            });
            Events.Add(new Calendar()
            {
                id = "required-id-3",
                name = "Columbus Day",
                badge = "Public",
                date = "October/12/2020",
                description = "United States Holiday",
                type = "event",
            });

            return Json(new { Events } );
        }

        private Calendar addEvent(int count, DateTime date, Course row)
        {
            return new Calendar()
            {
                id = "required-id-" + count,
                name = row.CourseName + "-" + row.CourseNumber,
                date = String.Format("{0:ddd MMM dd, yyyy}", date) + " " + row.StartTime.ToString(), //"Wed Jan 01 2020 00:00:00 GMT-0800 (Pacific Standard Time)",
                type = "event", // event, holiday, birthday
                everyYear = false,
                description = row.Description+" <a href='/Course/"+ row.ID+ "'>"+ row.CourseName + "-" + row.CourseNumber+"</a>"
            };
        }

    }

    public class Calendar
    {
        public string id { get; set; }
        public string name { get; set; }
        public string badge { get; set; }
        public string date { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public bool everyYear { get; set; }
        public string color { get; set; }
    }

}
