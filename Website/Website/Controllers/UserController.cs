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
        //private string login_type;
        
        // GET: User
        public ActionResult Index()
        {
            string login_type = Convert.ToString(Session["login_type"]).Trim();
            if (login_type != null) {
                if (login_type == "Student") { return RedirectToAction("Index","Student"); }
                if (login_type == "Librarian") { return RedirectToAction("Index", "Librarian"); }
                if (login_type == "Admin") { return RedirectToAction("Index", "Administrator"); }
            }
            return View();
            
        }

        [HttpGet]
        public ActionResult LogIn()
        {

            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Models.LibraryUser user)
        {
            if(user.IsValid(user.EmailID,user.Password))                            
            {
                FormsAuthentication.SetAuthCookie(user.EmailID, false);
                /*ViewBag.Message = login_type;
                return View();*/
                Session["login_type"] = user.login_type;
                Session["EmailID"] = user.EmailID;
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Log In Data is Incorrect");
            }
            return View();
        }

        
        public ActionResult LogOut()
        {
            Session.Clear();
            return View();
        }


        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Models.LibraryUser user)
        {
            if (user.StoreUser(user.EmailID, user.Password,user.FirstName,user.LastName))
            {
                return RedirectToAction("Index","User");
            }
            else
            {
                ModelState.AddModelError("", "Registration Data is Incorrect");
            }
            return View();
        }
        
        
    }
}