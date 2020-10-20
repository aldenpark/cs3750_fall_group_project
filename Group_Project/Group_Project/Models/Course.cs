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
        public int Monday { get; set; }
        //Is the class on mondays? 1 for yes, 0 for no
        public int Tuesday { get; set; }
        //Ditto, but for tuesdays
        public int Wednesday { get; set; }
        //Yada yada
        public int Thursday { get; set; }
        //etc, etc, and etc
        public int Friday { get; set; }
        public int Saturday { get; set; }
        public int Sunday { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Time")]
        public TimeSpan CourseTime { get; set; }
        //Time of day
        [Display(Name = "AM or PM")]
        public string AMPM { get; set; }
        //Literally whether the time is in "AM" or "PM"
        [Display(Name = "Max Students in Class")]
        public int MaxStudents { get; set; }


    }
}
