using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class AdministratorController : Controller
    {
        public ActionResult ViewIfAdminLoggedIn()
        {
            string login_type = Convert.ToString(Session["login_type"]).Trim();
            if (login_type != null)
            {
                if (login_type == "Student") { return RedirectToAction("Index", "Student"); }
                if (login_type == "Librarian") { return RedirectToAction("Index", "Librarian"); }
                if (login_type == "Admin") { return View(); }
                return RedirectToAction("LogOut", "User"); 
            }
                return RedirectToAction("Index","User");
        }
        // GET: Adminstrator
        public ActionResult Index()
        {
            return ViewIfAdminLoggedIn();
        }
    }
}