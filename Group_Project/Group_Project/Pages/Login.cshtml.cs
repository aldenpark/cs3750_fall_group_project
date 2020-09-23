using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages
{
    public class LoginModel : PageModel
    {
        public void OnPost()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];




        }
    }
}
