using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Football.Models
{
    public class Staff
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Length must be between 2-50")]
        public string firstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Length must be between 2-50")]
        public string lastName { get; set; }
        [Key]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Length must be between 2-50")]
        public string job { get; set; }
        [Required]
        [RegularExpression("^([2-7][0-9])$", ErrorMessage = "Must be a number between 20-79")]
        public string age { get; set; }
    }
}