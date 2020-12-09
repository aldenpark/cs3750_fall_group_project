using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Group_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : Controller
    {
        // GET: api/<NotificationsController>
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { test = "this will be a string of data" });
        }

        // DELETE api/<NotificationsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var test = id;
            return Json(new { success = true });
        }
    }
}
