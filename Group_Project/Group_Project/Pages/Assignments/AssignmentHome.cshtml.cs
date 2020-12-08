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

namespace Group_Project.Pages.Assignments
{
    public class AssignmentHomeModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private CourseHelper courseHelper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AssignmentHomeModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public User UserObj { get; set; }

        [BindProperty]
        public Models.Assignment Assignment { get; set; }

        [BindProperty]
        public List<Models.Submission> SubmissionObj { get; set; }


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

            Assignment = _unitOfWork.Assignment.GetFirstorDefault(m => m.ID == id);

            var Avglist = new List<int>();
            foreach(var item in _unitOfWork.Submission.GetAll(m => m.AssignmentId == Assignment.ID))
            {
                Avglist.Add(item.Points);
            }

            if (_unitOfWork.Submission.GetFirstorDefault(m => m.AssignmentId == Assignment.ID && m.UserId == userId) != null)
            {
                SubmissionObj = _unitOfWork.Submission.GetAll(m => m.AssignmentId == Assignment.ID && m.UserId == userId).ToList();
            }
            else
            {
                SubmissionObj = new List<Submission>();
                SubmissionObj.Add(new Submission());
            }
            
            Assignment.SubmissionId = SubmissionObj.LastOrDefault().ID;
            Assignment.Grade = SubmissionObj.LastOrDefault().Points;
            Assignment.Avg = "0";
            if (Avglist.Count() > 0)
                Assignment.Avg = Avglist.Average().ToString();
            courseHelper = new CourseHelper();
            

            if (Assignment == null)
            {
                return NotFound();
            }

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

            var AssignmentObj = _unitOfWork.Assignment.GetFirstorDefault(m => m.ID == Assignment.ID);
            SubmissionObj = _unitOfWork.Submission.GetAll(m => m.AssignmentId == Assignment.ID).ToList();

            var subm = new Submission();
            subm.AssignmentId = Assignment.ID;
            subm.UserId = UserObj.ID;
            subm.fileSubmitDisplay = Assignment.TextSubmission;

            if (AssignmentObj.SubmissionType != "Text Entry")
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files; // get, post, put, etc....

                subm.fileSubmitDisplay = SubmissionObj.Count > 0 ? String.Concat(files[0].FileName, " (", SubmissionObj.Count, ")") : files[0].FileName;

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"files\assignments");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    subm.fileSubmit = @"\files\assignments\" + fileName + extension;

                }
            }

            _unitOfWork.Submission.Add(subm);
            _unitOfWork.Save();

            return Redirect("./assignmentHome?id="+ Assignment.ID);
        }
    }
}
