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


        [BindProperty]
        public Models.User user { get; set; }


        public void OnPost()
        {
            System.Diagnostics.Debug.WriteLine(user);

        }



        public void OnGet()
        {

        }
    }
}
