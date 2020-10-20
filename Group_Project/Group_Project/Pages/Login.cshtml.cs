using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Group_Project.Data.Repository.IRepository;
using Group_Project.Helpers;
using Group_Project.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoginModel(IUnitOfWork unitOfWork)
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

            if (!ModelState.IsValid)
            {
                return Page();
            }

            //var user = await _context.User.FindAsync(id);
            var login = _unitOfWork.User.GetFirstorDefault(u => u.Email == user.Email);

            if (login == null)
            {
                return RedirectToPage("./BadLogin");
            }

            var encrypt = new Security();


            user.Passwrd = encrypt.EncryptString(user.Passwrd);
            //user.Passwrd = encrypt.HashPassword(user.Passwrd);

            if(encrypt.IsMatch(user.Passwrd,login.Passwrd) != true)
            {
                return RedirectToPage("./BadLogin");
            }

            // Setting Session with the id of the logged in User
            HttpContext.Session.SetInt32(SD.UserSessionId, login.ID);
            return RedirectToPage("/");
        }

    }
}
