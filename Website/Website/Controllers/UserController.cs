using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace Website.Controllers
{
    public class UserController : Controller
    {
        public ActionResult ViewIfNoOneLoggedIn()
        {
            string login_type = Convert.ToString(Session["login_type"]);
            if (login_type != null)
            {
                if (login_type == "Student") { return RedirectToAction("Index", "Student"); }
                if (login_type == "Librarian") { return RedirectToAction("Index", "Librarian"); }
                if (login_type == "Admin") { return RedirectToAction("Index", "Administrator"); }
            }
            return View();
        }
        //private string login_type;

        // GET: User
        public ActionResult Index()
        {
            return ViewIfNoOneLoggedIn();            
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return ViewIfNoOneLoggedIn();
        }

        [HttpPost]
        public ActionResult LogIn(Models.LibraryUser user)
        {
            if(user.Authenticate(user.EmailID,user.Password))                            
            {
                FormsAuthentication.SetAuthCookie(user.EmailID, false);
                /*ViewBag.Message = login_type;
                return View();*/
                Session["login_type"] = user.login_type;
                Session["EmailID"] = user.EmailID.Trim();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Log In Data is Incorrect");
            }
            return ViewIfNoOneLoggedIn();
        }

        
        public ActionResult LogOut()
        {
            Session.Clear(); Session.Abandon();
            return ViewIfNoOneLoggedIn();
        }


        [HttpGet]
        public ActionResult Registration()
        {
            return ViewIfNoOneLoggedIn();
        }

        [HttpPost]
        public ActionResult Registration(Models.LibraryUser user)
        {
            if (user.AddUser(user.EmailID, user.Password,user.FirstName,user.LastName))
            {
                return RedirectToAction("Index","User");
            }
            else
            {
                ModelState.AddModelError("", "Registration Data is Incorrect");
            }
            return ViewIfNoOneLoggedIn();
        }
    }
}