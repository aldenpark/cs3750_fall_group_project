using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group_Project.Models
{
    public class Notification
    {
        public int ID { get; set; }
        //ID of the notification itself

        public int sourceID { get; set; }
        //ID of the attached assignment or course

        public char Type { get; set; }
        //A single letter A for assignment created or C for assignment graded

        public string Message { get; set; }
        //The actual text in the Notification
    }
}
