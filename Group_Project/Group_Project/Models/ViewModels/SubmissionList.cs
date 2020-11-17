using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Models
{
    public class SubmissionList
    {
        public int ID { get; set; }
        public DateTime DueDate { get; set; }
        public string Student { get; set; }
    }
}
