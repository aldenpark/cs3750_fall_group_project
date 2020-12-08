using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Group_Project.Data;
using Group_Project.Models;
using Group_Project.Helpers;
using Microsoft.AspNetCore.Http;
using Group_Project.Utility;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Group_Project.Data.Repository.IRepository;

namespace Group_Project.Pages.Assignments.Grade
{
    public class AssignmentSubmissionModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private CourseHelper courseHelper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly Group_Project.Data.ApplicationDbContext _context;

        public AssignmentSubmissionModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, Group_Project.Data.ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        [BindProperty]
        public User UserObj { get; set; }

        [BindProperty]
        public Models.Assignment AssignmentObj { get; set; }

        [BindProperty]
        public Models.Submission SubmissionObj { get; set; }

  

        public IActionResult OnGet(int? id)
        {

            int userId = 0;
            if (HttpContext.Session.GetInt32(SD.UserSessionId) != null)
            {
                if (HttpContext.Session.GetInt32(SD.UserSessionId).HasValue)
                {
                    userId = HttpContext.Session.GetInt32(SD.UserSessionId).Value;
                }
            }
            UserObj = _unitOfWork.User.GetFirstorDefault(u => u.ID == userId);

            if (id == null)
            {
                return NotFound();
            }

            SubmissionObj = _unitOfWork.Submission.GetFirstorDefault(m => m.ID == id);
            if (SubmissionObj == null)
            {
                return NotFound();
            }

            AssignmentObj = _unitOfWork.Assignment.GetFirstorDefault(m => m.ID == SubmissionObj.AssignmentId);

            return Page();
        }

        public IActionResult OnPost()
        {
            int userId = 0;
            if (HttpContext.Session.GetInt32(SD.UserSessionId) != null)
            {
                if (HttpContext.Session.GetInt32(SD.UserSessionId).HasValue)
                {
                    userId = HttpContext.Session.GetInt32(SD.UserSessionId).Value;
                }
            }
            UserObj = _unitOfWork.User.GetFirstorDefault(u => u.ID == userId);

            if (UserObj.UserType == 'I')
            {
                Models.Notification NotificationObj = new Models.Notification();

                NotificationObj.sourceID = SubmissionObj.ID;
                NotificationObj.Type = 'A';
                NotificationObj.Message = "Assignment: " + AssignmentObj.Title + " Has been Created";

                
                _context.Notification.Add(NotificationObj);

                var objFromDb = _unitOfWork.Submission.Get(SubmissionObj.ID);
                objFromDb.Points = SubmissionObj.Points;
                _unitOfWork.Submission.Update(objFromDb);

                return Redirect("/Assignments/Grade?id="+ objFromDb.AssignmentId.ToString());
            }

            return NotFound();
        }
    }
}
