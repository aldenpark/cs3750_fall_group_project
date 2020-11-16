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

        public User User { get; set; }

        [BindProperty]
        public Models.Assignment Assignment { get; set; }

        [BindProperty]
        public List<Models.Submission> SubmissionObj { get; set; }

        public IActionResult OnGet(int? id)
        {

            //id = HttpContext.Session.GetInt32(SD.UserSessionId);

            if (id == null)
            {
                return NotFound();
            }

            Assignment = _unitOfWork.Assignment.GetFirstorDefault(m => m.ID == id);
            SubmissionObj = _unitOfWork.Submission.GetAll(m => m.AssignmentId == Assignment.ID).ToList();
            courseHelper = new CourseHelper();

            if (Assignment == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            int userId = 0;
            if (HttpContext.Session.GetInt32(SD.UserSessionId) != null)
            {
                if (HttpContext.Session.GetInt32(SD.UserSessionId).HasValue)
                {
                     userId = HttpContext.Session.GetInt32(SD.UserSessionId).Value;
                }
            }
            var AssignmentObj = _unitOfWork.Assignment.GetFirstorDefault(m => m.ID == Assignment.ID);
            SubmissionObj = _unitOfWork.Submission.GetAll(m => m.AssignmentId == Assignment.ID).ToList();

            var subm = new Submission();
            subm.AssignmentId = Assignment.ID;
            subm.UserId = userId;
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

            return RedirectToPage("./Index");
        }
    }
}
