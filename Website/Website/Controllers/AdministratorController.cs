using System;
using System.Web.Mvc;
using Website.Models;

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
        public ActionResult AddBook(Book book)
        {
            if (ModelState.IsValid)
            {
                String msg = book.AddBook(book);
                ViewBag.Message = msg;
            }
            return ViewIfAdminLoggedIn();
        }

        [HttpGet]
        public ActionResult AddLibrarian()
        {
            return ViewIfAdminLoggedIn();
        }

        [HttpPost]
        public ActionResult AddLibrarian(LibraryLibrarian librarian)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = Singleton.Instance.administrator.AddLibrarian(librarian);
            }
            return ViewIfAdminLoggedIn();
        }

        [HttpGet]
        public ActionResult UpdateDeleteBook()
        {
            ViewBag.Results = Singleton.Instance.book.AllBooks();
            return ViewIfAdminLoggedIn();
        }

        [HttpPost]
        public ActionResult UpdateDeleteBook(Book book)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form["delete"] == "yes") { ViewBag.Message = book.DeleteBook(book); }
                else { ViewBag.Message = book.UpdateBook(book); }
            }
            ViewBag.Results = book.AllBooks();
            return ViewIfAdminLoggedIn();
        }

        [HttpGet]
        public ActionResult UpdateDeleteLibrarian()
        {
            ViewBag.Results = Singleton.Instance.administrator.AllLibrarians();
            return ViewIfAdminLoggedIn();
        }

        [HttpPost]
        public ActionResult UpdateDeleteLibrarian(LibraryLibrarian librarian)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form["delete"] == "yes") { ViewBag.Message = Singleton.Instance.administrator.DeleteLibrarian(librarian); }
                else { ViewBag.Message = Singleton.Instance.administrator.UpdateLibrarian(librarian); }
            }
            ViewBag.Results = Singleton.Instance.administrator.AllLibrarians();
            return ViewIfAdminLoggedIn();
        }

    }
}