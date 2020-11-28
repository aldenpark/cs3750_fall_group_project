using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group_Project.Models
{
    public class Assignment
    {

        public int ID { get; set; }

        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        [Required]
        public DateTime DueDate { get; set; }
        [Display(Name = "Points Possible")]
        public int PointsPossible { get; set; }
        [Display(Name = "Submission Type")]
        public string SubmissionType { get; set; }
        //Either "Text Entry" or "File Submission", should be selected with a dropdown menu

        [NotMapped]
        public int SubmissionId { get; set; } // mapping used to get data in Assignment Submission
        [NotMapped]
        [Display(Name = "Text Entry")]
        public string TextSubmission { get; set; } // mapping used to get data in Assignment Submission

        [NotMapped]
        [Display(Name = "Grade")]
        public int Grade { get; set; } // mapping used to get data in Assignment Submission

        [NotMapped]
        [Display(Name = "Average")]
        public string Avg { get; set; } // mapping used to show class Average

    }
}
