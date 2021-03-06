using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Group_Project.Data.Repository.IRepository;
using Group_Project.Helpers;
using Group_Project.Models;
using Group_Project.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages.Profile
{

    public class EditModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EditModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public new User User { get; set; }

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

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files; // get, post, put, etc....

            var objFromDb = _unitOfWork.User.GetFirstorDefault(u => u.ID == User.ID);  // Only existing users can get to this page
            if (files.Count > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"img\profile");
                var extension = Path.GetExtension(files[0].FileName);

                if (objFromDb.ProfilePic != null)
                {
                    var imagePath = Path.Combine(webRootPath, objFromDb.ProfilePic.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                }

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                User.ProfilePic = @"\img\profile\" + fileName + extension;
            }
            else
            {
                User.ProfilePic = objFromDb.ProfilePic;  // no image uploaded, just readding it from db so we don't lose it
            }

            if(User.Passwrd != "")
            {
                var encrypt = new Security();
                User.Passwrd = encrypt.EncryptString(User.Passwrd);
                User.Passwrd = encrypt.HashPassword(User.Passwrd);
            }
            else
            {
                User.Passwrd = objFromDb.Passwrd;
            }

            _unitOfWork.User.Update(User);

            return RedirectToPage("./Index");
        }

    }
}
