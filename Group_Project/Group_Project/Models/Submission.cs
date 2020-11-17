using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group_Project.Models
{
    public class Submission
    {

        [Key]
        public int ID { get; set; }
        
        [Required]
        public int UserId { get; set; }

        [Required]
        public int AssignmentId { get; set; }
        [Display(Name = "Grade")]
        public int Points { get; set; }

        [Display(Name = "File Upload")]
        public string fileSubmit { get; set; } // filename of file on server, this needs to be unique because multiple submissions can be made but with the same filename
        [Display(Name = "File/Text")]
        public string fileSubmitDisplay { get; set; } // this displays either the uploaded filename or the text entered
        //should work for both Text Entry and File Submission
    }
}
