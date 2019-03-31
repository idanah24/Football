using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Football.Models
{
    public class User
    {
        [Key]
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string role { get; set; }

    }
}