using Group_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Helpers
{
    public class CourseHelper
    {
        public CourseValidationResponse ValidateCourse(Course course)
        {
            CourseValidationResponse response = new CourseValidationResponse();

            //Make sure the start time is not greater than the end time
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

        public string ConcatenateStartAndEndTime(Course course)
        {
            //Variable to hold the times of the course(StartTime & EndTime)
            string courseTimes = "";

            //Parse the startTime
            string[] startTime = course.StartTime.Split(':');

            //Get the hour of the startTime
            int startHour = int.Parse(startTime[0]);

            //Variable to hold the AM or PM part of the startTime
            string startAMPM = "";

            if (startHour > 12)
            {
                startAMPM = "PM";
                startHour = startHour - 12;
            }
            else
            {
                startAMPM = "AM";
            }

            //Save the start time
            courseTimes = startHour.ToString() + ":" + startTime[1] + " " + startAMPM;

            //Parse the endtime
            string[] endTime = course.EndTime.Split(':');

            //Get the hour of the endTime
            int endHour = int.Parse(endTime[0]);

            //Variable to hold the AM or PM part of the endTime
            string endAMPM = "";

            //If endHour is > 12 we need to convert the time from military to standard time
            if(endHour > 12)
            {
                endAMPM = "PM";
                endHour = endHour - 12;
            }
            else
            {
                endAMPM = "AM";
            }

            //concattenate the endTime to the start time to give a time interval
            courseTimes = courseTimes + " - " + endHour.ToString() + ":" + endTime[1] + " " + endAMPM;


            return courseTimes;
        }

        public string ConcatenateDaysAndTimes(Course course, string times)
        {
            string schedule = "";
            List<string> days = new List<string>();


            if (course.Sunday)
            {
                days.Add("Sun");
            }
            if (course.Monday)
            {
                days.Add("M");
            }
            if (course.Tuesday)
            {
                days.Add("T");
            }
            if (course.Wednesday)
            {
                days.Add("W");
            }
            if (course.Thursday)
            {
                days.Add("Th");
            }
            if (course.Friday)
            {
                days.Add("F");
            }
            if (course.Saturday)
            {
                days.Add("Sat");
            }

            for (int i = 0; i < days.Count; i++)
            {
                if (i != days.Count - 1)
                {
                    schedule = schedule + days[i] + "/";
                }
                else
                {
                    schedule = schedule + days[i] + " ";
                }
            }

            schedule = schedule + times;

            return schedule;
        }


    }

    public class CourseValidationResponse
    {
        public bool isValidated { get; set; }
        public string errorMessage { get; set; }
    }
}
