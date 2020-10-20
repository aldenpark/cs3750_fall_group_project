using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped] // don't map to field in db
        public string FullName { get { return FirstName + " " + LastName; } }


        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        [Required]
        public DateTime Birthdate { get; set; }
        [Display(Name = "User Type")]
        public char UserType { get; set; }
        //A single letter I for instructor, S for student

        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Zip Code")]
        public int Zip { get; set; }

        [Display(Name = "Bio")]
        public string Bio { get; set; }

        [Display(Name = "Link1")]
        public string Link1 { get; set; }

        [Display(Name = "Link2")]
        public string Link2 { get; set; }

        [Display(Name = "Link3")]
        public string Link3 { get; set; }

        public string ProfilePic { get; set; }

    }
}
