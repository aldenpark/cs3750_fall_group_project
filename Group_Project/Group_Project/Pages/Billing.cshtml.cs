using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Group_Project.Data;
using Group_Project.Models;
using Microsoft.AspNetCore.Http;
using Group_Project.Utility;
using System.ComponentModel.DataAnnotations;
using Group_Project.Helpers;
using System.Net;

namespace Group_Project.Pages
{
    public class BillingModel : PageModel
    {
        private readonly Group_Project.Data.ApplicationDbContext _context;
        private StripeHelper2 stripeHelper;
        private readonly int coursePrice = 100; 

        public BillingModel(Group_Project.Data.ApplicationDbContext context)
        {
            _context = context;
            stripeHelper = new StripeHelper2();
        }

        [ViewData]
        public User User { get; set; }
        public decimal balance { get; set; }
        public decimal amountDue { get; set; }

        [BindProperty]
        public BillingSubmission billingSubmission { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            id = HttpContext.Session.GetInt32(SD.UserSessionId);
            

            if (id == null)
            {
                return NotFound();
            }

            var isInstructor = await _context.User.Where(x => x.ID == id).Where(x => x.UserType == 'I').AnyAsync();

            if(isInstructor)
            {
                return NotFound();
            }

            User = await _context.User.FirstOrDefaultAsync(m => m.ID == id);
            decimal courseTotalPrice = (await _context.Registration.Where(x => x.StudentID == User.ID).CountAsync() * coursePrice);
            List<decimal> payments = await _context.StudentPayments.Where(x => x.StudentId == id).Select(x => x.Payment).ToListAsync();
            decimal totalPayments = payments.Sum();
            balance = courseTotalPrice;
            amountDue = (decimal)(courseTotalPrice - totalPayments);



            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            int id = (int)HttpContext.Session.GetInt32(SD.UserSessionId);
            PayBillResponse response = await stripeHelper.PayBill(billingSubmission);
            
            if(response.responseMessage.StatusCode != HttpStatusCode.OK)
            {
                return RedirectToPage("./Billing/PaymentFailed");
            }
            else
            {
                StudentPayment studentPayment = new StudentPayment();
                studentPayment.StudentId = id;
                studentPayment.StripeTokenId = response.studentPayments.StripeTokenId;
                studentPayment.Payment = response.studentPayments.Payment;
                studentPayment.CreatedOn = DateTime.UtcNow;
                await _context.StudentPayments.AddAsync(studentPayment);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Billing/PaymentSuccessful");
            }
        }
    }
}
