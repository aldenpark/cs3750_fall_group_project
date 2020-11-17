using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Group_Project.Data;
using Group_Project.Models;

namespace Group_Project.Pages.Assignments
{
    public class AssignmentListByCourseModel : PageModel
    {
        private readonly Group_Project.Data.ApplicationDbContext _context;

        public AssignmentListByCourseModel(Group_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Assignment> Assignment { get;set; }
        
        [ViewData]
        public Models.Course _course { get; set; }

        public async Task OnGetAsync(int courseId)
        {
            
            Assignment = await _context.Assignment.ToListAsync();
        }
    }
}
