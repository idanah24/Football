using Football.DAL;
using Football.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Football.Controllers
{

    /*This controller handles actions for unregistered user*/
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View(new Contact());
        }

        /*This method handles submiting new contact information*/
        public ActionResult SubmitContact(Contact cont)
        {
            DataLayer dal = new DataLayer();

            if (ModelState.IsValid)
            {
                if (contactExists(cont.email))
                {
                    ViewBag.message = "You have already submitted contact information";
                }
                else
                {   //Adding new contact information
                    dal.contacts.Add(cont);
                    dal.SaveChanges();
                    ViewBag.message = "Contact information was submitted succesfully";
                    cont = new Contact();
                }

            }
            else
            {
                ViewBag.message = "Contact information was not submitted";
            }
            return View("Contact", cont);
        }

        /*This function checks if user's email exists in contacts table*/
        public bool contactExists(string email)
        {
            DataLayer dal = new DataLayer();
            foreach(Contact contact in dal.contacts)
            {
                if (contact.email.Equals(email))
                    return true;
            }
            return false;
        }
    }
}