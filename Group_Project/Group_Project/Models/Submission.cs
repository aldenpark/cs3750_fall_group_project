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

        public string fileSubmit { get; set; }
        //should work for both Text Entry and File Submission
    }
}
