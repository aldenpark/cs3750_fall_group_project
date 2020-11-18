using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group_Project.Data.Repository.IRepository;
using Group_Project.Models;
using Group_Project.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Assignments
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Models.Assignment> ObjList { get; set; }
        public User UserObj { get; set; }

        public IActionResult OnGet(int id)
        {
            int userId = 0;
            if (HttpContext.Session.GetInt32(SD.UserSessionId) != null)
            {
                if (HttpContext.Session.GetInt32(SD.UserSessionId).HasValue)
                {
                    userId = HttpContext.Session.GetInt32(SD.UserSessionId).Value;
                }
            }
            if(userId < 1)
            {
                return RedirectToPage("/Login");
            }

            UserObj = _unitOfWork.User.GetFirstorDefault(u => u.ID == userId);
            ObjList = _unitOfWork.Assignment.GetAll(u => u.CourseID == id).ToList();

            return Page();
        }
    }
}
