using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        //obligatory primary key that we are unlikely to actually use
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Passwrd { get; set; }
        //"word" abbreviated on purpose to avoid a glitch I had earlier with another project, the hash should fit in a string
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        [Required]
        public DateTime Birthdate { get; set; }
        [Display(Name = "User Type")]
        public char UserType { get; set; }
        //A single letter I for instructor, S for student

    }
}
