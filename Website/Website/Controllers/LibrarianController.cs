using System;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class LibrarianController : Controller
    {
        public ActionResult ViewIfLibrarianLoggedIn()
        {
            String login_type = Convert.ToString(Session["login_type"]);
            if (login_type != null)
            {
                if (login_type == "Student") { return RedirectToAction("Index", "Student"); }
                if (login_type == "Librarian") { return View(); }
                if (login_type == "Admin") { return RedirectToAction("Index", "Administrator"); }
                return RedirectToAction("LogOut", "User");
            }
                return RedirectToAction("Index", "User");
        }
        // GET: Library
        public ActionResult Index()
        {
            return ViewIfLibrarianLoggedIn();
        }
        public ActionResult ReturnBook()
        {
            Models.LibraryLibrarian librarian = new Models.LibraryLibrarian(Convert.ToString(Session["EmailID"]));
            Session["Message"] = librarian.ReturnBook(Request.QueryString["ISBN"], Request.QueryString["emailid"]);
            return RedirectToAction("AllCheckedOut", "Librarian");
        }
        public ActionResult AllCheckedOut()
        {
            Models.LibraryLibrarian librarian = new Models.LibraryLibrarian(Convert.ToString(Session["EmailID"]));
            ViewBag.Results = librarian.AllCheckedOut();
            return ViewIfLibrarianLoggedIn();
        }
    }
}