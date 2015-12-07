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
            string login_type = Convert.ToString(Session["login_type"]);
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

        [HttpGet]
        public ActionResult AddBook()
        {
            return ViewIfAdminLoggedIn();
        }

        [HttpPost]
        public ActionResult AddBook(Models.LibraryAdministrator admin)
        {
            string msg = admin.AddBook();
            ViewBag.Message = msg;
            return ViewIfAdminLoggedIn();
        }

        [HttpGet]
        public ActionResult AddLibrarian()
        {
            return ViewIfAdminLoggedIn();
        }

        [HttpPost]
        public ActionResult AddLibrarian(Models.LibraryAdministrator admin)
        {
            string msg = admin.AddLibrarian();
            ViewBag.Message = msg;
            
            return ViewIfAdminLoggedIn();
        }

        [HttpGet]
        public ActionResult UpdateDeleteBook()
        {
            Models.LibraryAdministrator admin = new Models.LibraryAdministrator(Convert.ToString(Session["EmailID"]));
            ViewBag.Results = admin.AllBooks();
            return ViewIfAdminLoggedIn();
        }

        [HttpPost]
        public ActionResult UpdateDeleteBook(Models.LibraryAdministrator admin)
        {
            string msg = "Default";
            msg = admin.UpdateBook(Request.QueryString["ISBN"],admin.Quantity,admin.Location);
            ViewBag.Message = msg;
            return ViewIfAdminLoggedIn();
        }

        [HttpGet]
        public ActionResult UpdateBook()
        {
            Models.LibraryAdministrator admin = new Models.LibraryAdministrator();
            Session["Message"] = admin.UpdateBook(Request.QueryString["ISBN"], admin.Quantity, admin.Location);
            return RedirectToAction("UpdatedBook");
        }

        [HttpGet]
        public ActionResult UpdatedBook(Models.LibraryAdministrator admin)
        {

            ViewBag.Book = admin.SearchISBN(Convert.ToString(Session["Message"]));
            
            return ViewIfAdminLoggedIn();
        }
    }
}