using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Group_Project.Data.Repository.IRepository;
using Group_Project.Helpers;
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
                return NotFound();
            }

            var encrypt = new Security();
            var encPasswrd = encrypt.HashPassword(login.Passwrd);
            if(!encrypt.IsMatch(login.Passwrd, encPasswrd))
            {
                return Forbid();
            }

            // Do we have a page for showing a sucessful login?
            return RedirectToPage("./LoginSuccess");
        }

    }
}
