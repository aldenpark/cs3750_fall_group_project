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

//https://localhost:44329/Assignments/Grade/AssignmentDetail?id=1

namespace Group_Project.Pages.Assignments
{
    public class AssignmentDetailModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssignmentDetailModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public User UserObj { get; set; }

        [BindProperty]
        public AssignmentGradeVM AssignmentGradeObj { get; set; }

        public IActionResult OnGet(int? id)
        {

            AssignmentGradeObj = new AssignmentGradeVM();
            int userId = 0;
            if (HttpContext.Session.GetInt32(SD.UserSessionId) != null)
            {
                if (HttpContext.Session.GetInt32(SD.UserSessionId).HasValue)
                {
                    userId = HttpContext.Session.GetInt32(SD.UserSessionId).Value;
                }
            }
            UserObj = _unitOfWork.User.GetFirstorDefault(u => u.ID == userId && u.UserType == 'I');
            if (UserObj == null)
            {
                return RedirectToPage("/BadLogin");
            }

            if (id == null)
            {
                return NotFound();
            }


            AssignmentGradeObj.AssignmentObj = _unitOfWork.Assignment.GetFirstorDefault(m => m.ID == id);
            if (AssignmentGradeObj.AssignmentObj == null)
            {
                return NotFound();
            }

            var SubmissionsList = _unitOfWork.Submission.GetAll(m => m.AssignmentId == AssignmentGradeObj.AssignmentObj.ID).OrderBy(s => s.ID);
            var Students = _unitOfWork.Registration.GetAll(s => s.CourseID == AssignmentGradeObj.AssignmentObj.CourseID && s.Status == ClassTypes.Registered);
            var Avglist = new List<int>();
            AssignmentGradeObj.StudentList = new List<StudentListGradeVM>();
            foreach (var item in Students)
            {
                var StudentObj = new StudentListGradeVM();
                StudentObj.Student = _unitOfWork.User.GetFirstorDefault(m => m.ID == item.StudentID);
                foreach(var subm in SubmissionsList)
                {
                    if(subm.UserId == StudentObj.Student.ID)
                    {
                        StudentObj.Grade = subm.Points;
                        StudentObj.submitted = true;
                        Avglist.Add(subm.Points);
                    }
                }
                AssignmentGradeObj.StudentList.Add(StudentObj);
            }

            if(AssignmentGradeObj.StudentList.Count() > 0)
            {
                foreach (var item in AssignmentGradeObj.StudentList)
                {
                    AssignmentGradeObj.jsGradesList += "{ y: " + item.Grade.ToString() + ", label: \"" + item.Student.FullName + (item.submitted == false ? " (NOT SUBMITTED) " : "") + "\" },";
                }
            }

            AssignmentGradeObj.AssignmentObj.Avg = "0";
            if (Avglist.Count() > 0)
            {
                AssignmentGradeObj.AssignmentObj.Avg = Avglist.Average().ToString();
            }

            return Page();
        }

    }
}
