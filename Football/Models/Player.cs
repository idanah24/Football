using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Football.Models
{
    public class Player
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Length must be between 2-50")]
        public string firstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Length must be between 2-50")]
        public string lastName { get; set; }
        [Required]
        [RegularExpression("^(100|[1-9][0-9]?)$", ErrorMessage = "Must be a number between 1-100")]
        public string rating { get; set; }
        [Key]
        [Required]
        [RegularExpression("^([1-9][0-9]?)$", ErrorMessage = "Must be a number between 1-99")]
        public string number { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "Length must be between 2-3")]
        public string position { get; set; }

    }
}