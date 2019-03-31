using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Football.Models
{
    public class Contact
    {

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Length must be between 2-50")]
        public string name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }

        [Key]
        [Required]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Length must be between 2-255")]
        public string message { get; set; }


    }
}