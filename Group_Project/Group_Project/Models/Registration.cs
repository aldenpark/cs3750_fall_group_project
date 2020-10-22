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
        //The ID of the registration
        public int StudentID { get; set; }
        //The ID of the student
        public int CourseID { get; set; }
        //The ID of the Course

        //This model exists solely to link students to courses
    }
}
