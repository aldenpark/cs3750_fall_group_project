using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Models
{
    public class Registration
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public ClassTypes Status { get; set; } = ClassTypes.Registered;
        public bool paid { get; set; } = false;
    }

    public enum ClassTypes
    {
        Registered = 0,
        Withdrawn = 1,
        Passed = 2
    }
}
