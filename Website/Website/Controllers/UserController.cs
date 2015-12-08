using System;
using System.Web.Mvc;
using System.Web.Security;

namespace Website.Controllers
{
    public class UserController : Controller
    {
        public ActionResult ViewIfNoOneLoggedIn()
        {
            String login_type = Convert.ToString(Session["login_type"]);
            if (login_type != null)
            {
                if (login_type == "Student") { return RedirectToAction("Index", "Student"); }
                if (login_type == "Librarian") { return RedirectToAction("Index", "Librarian"); }
                if (login_type == "Admin") { return RedirectToAction("Index", "Administrator"); }
            }
            return View();
        }
        //private String login_type;

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
            if (ModelState.IsValid)
            {
                if (user.Authenticate(user))
                {
                    FormsAuthentication.SetAuthCookie(user.EmailID, false);
                    Session["login_type"] = user.login_type;
                    Session["EmailID"] = user.EmailID.Trim();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Log In Data is Incorrect");
                }
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
            if (ModelState.IsValid)
            {
                if (user.AddUser(user))
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Duplicate Email ID used try again with another Email ID");
                }
            }
            return ViewIfNoOneLoggedIn();
        }
    }
}