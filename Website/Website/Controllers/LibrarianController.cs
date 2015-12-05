using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class LibrarianController : Controller
    {
        public ActionResult ViewIfLibrarianLoggedIn()
        {
            string login_type = Convert.ToString(Session["login_type"]).Trim();
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
            Models.LibraryLibrarian librarian = new Models.LibraryLibrarian();
            Session["Message"] = librarian.ReturnBook(Request.QueryString["ISBN"], Request.QueryString["emailid"]);
            return RedirectToAction("AllCheckedOut", "Librarian");
        }
        public ActionResult AllCheckedOut()
        {
            Models.LibraryLibrarian librarian = new Models.LibraryLibrarian();
            ViewBag.Results = librarian.AllCheckedOut();
            return ViewIfLibrarianLoggedIn();
        }
    }
}