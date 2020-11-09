using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group_Project.Data.Repository.IRepository;
using Group_Project.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stripe.Terminal;

namespace Group_Project.Pages.Registration
{
    public class IndexModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Models.Course CourseObj { get; set; }
        [BindProperty]
        public bool ValidUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int userId = 0;
            if (HttpContext.Session.GetInt32(SD.UserSessionId) != null)
            {
                if (HttpContext.Session.GetInt32(SD.UserSessionId).HasValue)
                {
                    userId = HttpContext.Session.GetInt32(SD.UserSessionId).Value;
                }
            }

            ValidUser = false;
            if (userId > 0)
            {
                ValidUser = true;
                var userObj = _unitOfWork.User.GetFirstorDefault(u => u.ID == userId);
                if (userObj != null && userObj.UserType == 'I')
                {
                    return NotFound();
                }
            }

            return Page();
        }
    }
}
