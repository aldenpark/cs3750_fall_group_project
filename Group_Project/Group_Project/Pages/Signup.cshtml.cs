using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group_Project.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Group_Project.Helpers;

namespace Group_Project.Pages
{
    public class SignupModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public SignupModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Models.User user { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            //System.Diagnostics.Debug.WriteLine(user);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (user.ID == 0)
            {
                var encrypt = new Security();
                user.Passwrd = encrypt.EncryptString(user.Passwrd);
                user.Passwrd = encrypt.HashPassword(user.Passwrd);

                _unitOfWork.User.Add(user);
                _unitOfWork.Save(); // Create new user
            }

            // I'd image we'll redirect them to a different page on the next part of our project but just for testing going to send new users to our Login page
            return RedirectToPage("./Login");
        }



    }
}
