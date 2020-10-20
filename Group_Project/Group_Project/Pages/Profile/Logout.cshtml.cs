using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group_Project.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Profile
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            //HttpContext.Session.SetInt32(SD.UserSessionId, 0);  // Set logged in user to 0
            HttpContext.Session.Clear(); // Delete all sessions
            return RedirectToPage("/");
        }
    }
}
