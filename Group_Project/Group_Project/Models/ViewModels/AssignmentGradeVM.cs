using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Models
{
    public class AssignmentGradeVM
    {
        public List<StudentListGradeVM> StudentList { get; set; }
        public Assignment AssignmentObj { get; set; }
        public string jsGradesList { get; set; }
    }
    public class StudentListGradeVM
    {
        public User Student { get; set; }
        public double Grade { get; set; } = 0;
        public bool submitted { get; set; } = false;
    }
}
