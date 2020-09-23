using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages
{
    public class SignupModel : PageModel
    {
        public void OnPost()
        {

            var username = Request.Form["username"];
            var password = Request.Form["password"];
            var fName = Request.Form["fname"];
            var lName = Request.Form["lname"];
            var birthDate = Request.Form["birthdate"];
            var email = Request.Form["email"];

            Models.User login = new Models.User();

        }
    }
}
