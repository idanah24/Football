using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Football.Models;

namespace Football.ViewModels
{

    /*This class is a general view model*/
    public class ViewModel
    {
        public List<Player> players { get; set; }
        public Player player { get; set; }
        public List<Staff> staffs { get; set; }
        public Staff staff { get; set; }
        public List<User> users { get; set; }
        public User user { get; set; }

        public List<Contact> contacts { get; set; }
    }
}