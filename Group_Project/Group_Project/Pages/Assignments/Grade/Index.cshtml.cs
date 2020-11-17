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

namespace Group_Project.Pages.Assignments.Grade
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Assignment AssignmentObj { get; set; }
        public List<Models.SubmissionList> ObjList { get; set; }
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
            UserObj = _unitOfWork.User.GetFirstorDefault(u => u.ID == userId && u.UserType == 'I');
            if (UserObj.ID < 1)
            {
                return RedirectToPage("/BadLogin");
            }

            AssignmentObj = _unitOfWork.Assignment.Get(id);

            var result = from s in _unitOfWork.Submission.GetAll(s => s.AssignmentId == id)
                         join a in _unitOfWork.Assignment.GetAll() on s.AssignmentId equals a.ID
                         join u in _unitOfWork.User.GetAll() on s.UserId equals u.ID
                         //where a.DueDate >= DateTime.Now
                         orderby a.ID
                         select new SubmissionList()
                         {
                             ID = a.ID,
                             DueDate = a.DueDate,
                             Student = u.FullName,
                         };

            ObjList = result.ToList();
            return Page();
        }
    }
}
