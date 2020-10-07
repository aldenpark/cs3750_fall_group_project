using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Group_Project.Data;
using Group_Project.Data.Repository.IRepository;
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
            var Events = new List<Calendar>();
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
