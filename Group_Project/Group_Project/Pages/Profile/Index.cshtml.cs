using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group_Project.Data.Repository.IRepository;
using Group_Project.Models;
using Group_Project.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Group_Project.Pages.Profile
{


    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public IndexModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty] 
        public new User User { get; set; }
        public Models.Course Course { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32(SD.UserSessionId) != null)
            {
                if (HttpContext.Session.GetInt32(SD.UserSessionId).HasValue)
                {
                    int userId = HttpContext.Session.GetInt32(SD.UserSessionId).Value;
                    User = _unitOfWork.User.Get(userId);
                    if (User != null)
                    {
                        return Page();
                    }
                }
            }

            return RedirectToPage("/Login");
        }
    }
}
