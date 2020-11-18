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

            //Aldens Code
            /*
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
            */

            //Arics Code
            var submitters = (from s in _unitOfWork.Submission.GetAll(s => s.AssignmentId == id) group s by s.UserId into g select g.Last()).ToList();

            if(ObjList == null)
            {
                ObjList = new List<SubmissionList>();
            }

            int i = 0;
            foreach(var submission in submitters)
            {
                ObjList.Add(new SubmissionList());
                ObjList[i].ID = submission.ID; 
                ObjList[i].DueDate = _unitOfWork.Assignment.GetAll(x => x.ID == submission.ID).Select(x => x.DueDate).FirstOrDefault();
                ObjList[i].Student = _unitOfWork.User.GetAll(x => x.ID == submission.UserId).Select(x => x.FirstName + x.LastName).FirstOrDefault() ?? String.Empty;
                i++;
            }



            
            return Page();
        }
    }
}
