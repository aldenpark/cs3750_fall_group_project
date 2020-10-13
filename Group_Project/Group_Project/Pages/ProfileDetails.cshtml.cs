using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Group_Project.Data;
using Group_Project.Models;


namespace Group_Project.Pages
{
    public class ProfileDetailsModel : PageModel
    {

        private readonly Group_Project.Data.ApplicationDbContext _context;


        [BindProperty]
        public User User { get; set; }



        public void OnGet()
        {
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.ID == id);
        }

    }
}
