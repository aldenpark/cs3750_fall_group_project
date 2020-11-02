using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Group_Project.Data;
using Group_Project.Models;
using Microsoft.AspNetCore.Http;
using Group_Project.Utility;
using System.ComponentModel.DataAnnotations;
using Group_Project.Helpers;

namespace Group_Project.Pages
{
    public class BillingModel : PageModel
    {
        private readonly Group_Project.Data.ApplicationDbContext _context;
        private StripeHelper2 stripeHelper;

        public BillingModel(Group_Project.Data.ApplicationDbContext context)
        {
            _context = context;
            stripeHelper = new StripeHelper2();
        }

        [ViewData]
        public User User { get; set; }
        public int balance { get; set; }

        [BindProperty]
        public BillingSubmission billingSubmission { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            id = HttpContext.Session.GetInt32(SD.UserSessionId);

            if (id == null)
            {
                return NotFound();
            }

            User = await _context.User.FirstOrDefaultAsync(m => m.ID == id);
            balance = (await _context.Registration.Where(x => x.StudentID == User.ID).CountAsync() * 500);


            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            return Page();
        }
    }
}
