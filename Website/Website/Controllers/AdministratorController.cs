using System;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class AdministratorController : Controller
    {
        public ActionResult ViewIfAdminLoggedIn()
        {
            String login_type = Convert.ToString(Session["login_type"]);
            if (login_type != null)
            {
                if (login_type == "Student") { return RedirectToAction("Index", "Student"); }
                if (login_type == "Librarian") { return RedirectToAction("Index", "Librarian"); }
                if (login_type == "Admin") { return View(); }
                return RedirectToAction("LogOut", "User");
            }
            return RedirectToAction("Index", "User");
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
        public ActionResult AddBook(Models.Book book)
        {
            String msg = book.AddBook(book);
            ViewBag.Message = msg;
            return ViewIfAdminLoggedIn();
        }

        [HttpGet]
        public ActionResult AddLibrarian()
        {
            return ViewIfAdminLoggedIn();
        }

        [HttpPost]
        public ActionResult AddLibrarian(Models.LibraryLibrarian librarian)
        {
            Models.LibraryAdministrator admin = new Models.LibraryAdministrator(Convert.ToString(Session["EmailID"]));
            ViewBag.Message = admin.AddLibrarian(librarian);
            return ViewIfAdminLoggedIn();
        }

        [HttpGet]
        public ActionResult UpdateDeleteBook()
        {
            Models.Book book = new Models.Book();
            ViewBag.Results = book.AllBooks();
            return ViewIfAdminLoggedIn();
        }

        [HttpPost]
        public ActionResult UpdateDeleteBook(Models.Book book)
        {
            if (Request.Form["delete"] == "yes") { ViewBag.Message = book.DeleteBook(book); }
            else { ViewBag.Message = book.UpdateBook(book); }
            ViewBag.Results = book.AllBooks();
            return ViewIfAdminLoggedIn();
        }

        [HttpGet]
        public ActionResult UpdateDeleteLibrarian()
        {
            Models.LibraryAdministrator admin = new Models.LibraryAdministrator(Convert.ToString(Session["EmailID"]));
            ViewBag.Results = admin.AllLibrarians();
            return ViewIfAdminLoggedIn();
        }

        [HttpPost]
        public ActionResult UpdateDeleteLibrarian(Models.LibraryLibrarian librarian)
        {
            Models.LibraryAdministrator admin = new Models.LibraryAdministrator(Convert.ToString(Session["EmailID"]));
            if (Request.Form["delete"] == "yes") { ViewBag.Message = admin.DeleteLibrarian(librarian); }
            else { ViewBag.Message = admin.UpdateLibrarian(librarian); }
            ViewBag.Results = admin.AllLibrarians();
            return ViewIfAdminLoggedIn();
        }

    }
}