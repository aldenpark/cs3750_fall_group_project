using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Models
{
    public class SearchCourses
    {
        public string CourseName { get; set; }
        public string CourseNumber { get; set; }
        public string Department { get; set; }
        public string CreditHours { get; set; }
    }
}
