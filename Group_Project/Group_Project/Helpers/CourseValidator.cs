using Group_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Helpers
{
    public class CourseValidator
    {
        public CourseValidationResponse ValidateCourse(Course course)
        {
            CourseValidationResponse response = new CourseValidationResponse();

            if(DateTime.Parse(course.StartTime) > DateTime.Parse(course.EndTime))
            {
                response.isValidated = false;
                response.errorMessage = "Invalid start or endtime";
            }
            else
            {
                response.isValidated = true;
            }

            return response;
        }
    }

    public class CourseValidationResponse
    {
        public bool isValidated { get; set; }
        public string errorMessage { get; set; }
    }
}
