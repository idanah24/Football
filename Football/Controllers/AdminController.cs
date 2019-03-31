using Football.Classes;
using Football.DAL;
using Football.Models;
using Football.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Football.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddPlayer()
        {
            ViewBag.PlayerError = "";
            Player player = new Player();
            return View(player);
        }
        [HttpPost]
        public ActionResult SubmitPlayer()
        {
            //Getting Player information from form
            Player obj = new Player();
            obj.number      = Request.Form["number"].ToString();
            obj.firstName   = Request.Form["firstName"].ToString();
            obj.lastName    = Request.Form["lastName"].ToString();
            obj.position    = Request.Form["position"].ToString();
            obj.rating      = Request.Form["rating"].ToString();
            DataLayer dal = new DataLayer();

            if (ModelState.IsValid)
            {
                if (NumberExists(obj.number)) //Checks if player number (key) exists in database
                {
                    ViewBag.PlayerError = "Number exists in Database .";
                    return View("AddPlayer", obj);
                }
                //Adding player to database
                dal.players.Add(obj);
                dal.SaveChanges();
                return View("AdminInterface");
            }
            else//form is invalid
                return View("AddPlayer",obj);
        }
        //this function checks if number exists in  database
        private bool NumberExists(string number)
        {
            DataLayer dal = new DataLayer();
            foreach (Player player in dal.players)
            {
                if (number.Equals(player.number))
                    return true;
            }
            return false;
        }
        public ActionResult AddStaff()
        {
            ViewBag.StaffError = "";
            Staff staff = new Staff();
            return View(staff);
        }
        [HttpPost]
        public ActionResult SubmitStaff()
        {
            //Getting staff information from form
            Staff obj = new Staff();
            obj.job = Request.Form["job"].ToString();
            obj.firstName = Request.Form["firstName"].ToString();
            obj.lastName = Request.Form["lastName"].ToString();
            obj.age = Request.Form["age"].ToString();
            DataLayer dal = new DataLayer();

            if (ModelState.IsValid)
            {
                if (JobExists(obj.job))//checks if job(key) exists in database
                {
                    ViewBag.StaffError = "Job exists in database .";
                    return View("AddStaff", obj);
                }
                //adding staff to database
                dal.staffs.Add(obj);
                dal.SaveChanges();
                return View("AdminInterface");
            }
            else
                return View("AddStaff", obj);
        }
        //this function checks if job exists in database
        private bool JobExists(string job)
        {
            DataLayer dal = new DataLayer();
            foreach (Staff staff in dal.staffs)
            {
                if (job.Equals(staff.job))
                    return true;
            }
            return false;
        }
        
        public ActionResult AdminInterface()
        {
            return View();
        }
        //this action shows the initial search player page
        public ActionResult ShowSearchPlayers()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            vm.players = dal.players.ToList<Player>();
            return View("SearchPlayers", vm);
        }
        //this function finds the matching search values in database and shows them
        [HttpPost]
        public ActionResult SearchPlayers()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            //getting search value from form
            string searchValue = Request.Form["srcFirstName"].ToString();
            //finding matching players by firstname
            List<Player> players =
                (from x in dal.players
                 where x.firstName.Contains(searchValue)
                 select x).ToList<Player>();
            vm.players = players;
            return View(vm);
        }
        //this function deletes player from database
        [HttpPost]
        public ActionResult DeletePlayer()
        {
            DataLayer dal = new DataLayer();
            //getting the number of player to delete
            string number = Request.Form["delNumber"].ToString();
            foreach(Player player in dal.players)
            {
                if (player.number.Equals(number))
                {
                    //removing player from list
                    dal.players.Remove(player);
                }
            }
            //updating the database
            dal.SaveChanges();
            ViewModel vm = new ViewModel();
            vm.players = dal.players.ToList<Player>();
            //directing back to search players without the deleted player
            return View("SearchPlayers", vm);
        }
        //this action shows the initial search staff page
        public ActionResult ShowSearchStaff()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            vm.staffs = dal.staffs.ToList<Staff>();
            return View("SearchStaff", vm);
        }
        //this function finds the matching search values in database and shows them
        [HttpPost]
        public ActionResult SearchStaff()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            //getting the search value from form
            string searchValue = Request.Form["srcFirstName"].ToString();
            //finding matching staff from database
            List<Staff> staffs =
                (from x in dal.staffs
                 where x.firstName.Contains(searchValue)
                 select x).ToList<Staff>();
            vm.staffs = staffs;
            return View(vm);
        }
        //this function deletes staff from database
        [HttpPost]
        public ActionResult DeleteStaff()
        {
            DataLayer dal = new DataLayer();
            //getting the staff's job from form
            string job = Request.Form["delJob"].ToString();
            foreach (Staff staff in dal.staffs)
            {
                //finding the staff to delete
                if (staff.job.Equals(job))
                {
                    //removing staff from list
                    dal.staffs.Remove(staff);
                }
            }
            //updating database
            dal.SaveChanges();
            ViewModel vm = new ViewModel();
            vm.staffs = dal.staffs.ToList<Staff>();
            return View("SearchStaff", vm);
        }
        //this functions shows the admin registration page with list of admins only
        public ActionResult AdminRegister()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            User user = new User();
            //getting only admins from user database
            List<User> objAdmins = (from x in dal.users
                                    where x.role.Contains("Admin")
                                    select x).ToList<User>();
            vm.user = user;
            vm.users = objAdmins;
            ViewBag.AdminLoginError = "";
            return View(vm);
        }
        //adding new admin to database
        public ActionResult AddAdmin(User user)
        {
            DataLayer dal = new DataLayer();
            Encryption enc = new Encryption();
            //checking if form is valid
            if (ModelState.IsValid)
            {
                //generating hashed password
                string hashedPassword = enc.CreateHash(user.password);
                //checks if admin exists in database
                if (!adminExists(user.userName)) { 
                    //updating password to hashed password
                    user.password = hashedPassword;
                    //adding user to database
                    dal.users.Add(user);
                    dal.SaveChanges();
                    ViewBag.message = "Admin was added succesfully.";
                    user = new User();
                }
                else
                    ViewBag.message = "Username Exists in database.";
            }
            else
                ViewBag.message = "Error in registration.";
            ViewModel vm = new ViewModel();
            //getting updated list of admins
            List<User> objAdmins = (from x in dal.users
                                    where x.role.Contains("Admin")
                                    select x).ToList<User>();
            vm.users = objAdmins;
            vm.user = user;
            //returning admin list in json format
            return Json(objAdmins, JsonRequestBehavior.AllowGet);
        }
        //checks if admin exists in database by username
        private bool adminExists(string userName)
        {
            DataLayer dal = new DataLayer();
            List<User> users = dal.users.ToList<User>();
            foreach (User user in dal.users)
                if (user.userName.Equals(userName))
                    return true;
            return false;
        }
        //this function makes a list of admins and return in a json format
        public ActionResult GetAdminsByJson()
        {
            DataLayer dal = new DataLayer();
            List<User> objAdmins = (from x in dal.users
                                    where x.role.Contains("Admin")
                                    select x).ToList<User>();
            return Json(objAdmins, JsonRequestBehavior.AllowGet);
        }
        //this function shows the contacts from database
        public ActionResult ShowContacts()
        {
            DataLayer dal = new DataLayer();
            List<Contact> objContacts = dal.contacts.ToList<Contact>();
            ViewModel vm = new ViewModel();
            vm.contacts = objContacts;
            return View(vm);
        }
    }
}