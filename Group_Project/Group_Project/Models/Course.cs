using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Models
{
    public class Course
    {
        public int ID { get; set; }
        //The ID of the course NOT the instructor!
        public int InstructorID{ get; set; }
        //Same as the ID of the instructor, should ideally be a foreign key, 
        //but might not be neccessary to program it as such
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }
        [Display(Name = "Course Number")]
        public int CourseNumber { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        [Display(Name = "Credit Hours")]
        public int CreditHours { get; set; }
        public string Location { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        
        //Time of day
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public string StartTime { get; set; }
        
        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public string EndTime { get; set; }

        [Display(Name = "Max Students in Class")]
        public int MaxStudents { get; set; }


    }
}
