using Football.Classes;
using Football.DAL;
using Football.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Football.Controllers
{

    /*This controller handles user's sign in/sign out & user(not admin) register*/
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        /*This function redirects to user login page*/
        public ActionResult UserLogin()
        {
            User user = new User();
            ViewBag.UserLoginMessage = "";
            return View(user);
        }

        /*This function handles user login*/
        /*Given information from user login form*/
        public ActionResult Login(User user)
        {
            
            DataLayer dal = new DataLayer();
            Encryption enc = new Encryption();
            List<User> userToCheck = (from x in dal.users
                                        where x.userName == user.userName
                                        select x).ToList<User>();       //Attempting to get user information from database
            if (userToCheck.Count != 0)     //In case username was found
            {
                if (enc.ValidatePassword(user.password, userToCheck[0].password))   //Correct password
                {
                    var authTicket = new FormsAuthenticationTicket(
                        1,                                  // version
                        user.userName,                      // user name
                        DateTime.Now,                       // created
                        DateTime.Now.AddMinutes(20),        // expires
                        true,       //keep me connected
                        userToCheck[0].role                       // store roles
                        );

                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);
                    return RedirectToRoute("HomePage");
                }
                else
                {
                    ViewBag.UserLoginMessage = "Incorrect Username/password";
                }
            }
            else
                ViewBag.UserLoginMessage = "Incorrect Username/password";
            return View("UserLogin", user);
        }

        /*This function handles signing out*/
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("HomePage");
        }

        /*This function redirects to user register page*/
        public ActionResult UserRegister()
        {
            User user = new User();
            ViewBag.UserLoginError = "";
            return View(user);
        }

        /*This function adds new user to database*/
        /*Given user information from user register form*/
        public ActionResult AddUser(User user)
        {
            DataLayer dal = new DataLayer();
            Encryption enc = new Encryption();

            if (ModelState.IsValid)
            {
                string hashedPassword = enc.CreateHash(user.password);      //Encrypting user's password
                if (!userExists(user.userName))     //Adding user to database
                {
                    user.password = hashedPassword;
                    dal.users.Add(user);
                    dal.SaveChanges();
                    ViewBag.message = "User was added succesfully.";
                    user = new User();
                }
                else
                    ViewBag.message = "Username Exists in database.";
            }
            else
                ViewBag.message = "Error in registration.";
            return View("UserRegister", user);
        }

        /*This function compares given username with usernames in database*/
        private bool userExists(string userName)
        {
            DataLayer dal = new DataLayer();
            List<User> users = dal.users.ToList<User>();
            foreach (User user in dal.users)
                if (user.userName.Equals(userName))
                    return true;
            return false;
        }
    }
}