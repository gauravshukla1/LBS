using System;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult ViewIfStudentLoggedIn()
        {
            String login_type = Convert.ToString(Session["login_type"]);
            if (login_type != null)
            {
                if (login_type == "Student") { return View(); }
                if (login_type == "Librarian") { return RedirectToAction("Index", "Librarian"); }
                if (login_type == "Admin") { return RedirectToAction("Index", "Administrator"); }
                return RedirectToAction("LogOut", "User");
            }
            return RedirectToAction("Index", "User");
        }
        // GET: Student
        public ActionResult Index()
        {
            LibraryStudent student = new LibraryStudent(Convert.ToString(Session["EmailID"]));
            ViewBag.Message = student.FirstName + " " + student.LastName;
            return ViewIfStudentLoggedIn();
        }

        [HttpGet]
        public ActionResult Search()
        {
            ViewBag.Results = Singleton.Instance.book.Search(Request.QueryString["Term"], Request.QueryString["Criteria"]);
            return ViewIfStudentLoggedIn();
        }

        [HttpGet]
        public ActionResult CheckOut()
        {
            LibraryStudent student = new LibraryStudent(Convert.ToString(Session["EmailID"]));
            Session["Message"] = student.CheckOut(Request.QueryString["ISBN"]);
            return RedirectToAction("AllCheckedOut", "Student");
        }

        public ActionResult AllCheckedOut()
        {
            LibraryStudent student = new LibraryStudent(Convert.ToString(Session["EmailID"]));
            ViewBag.Results = student.AllCheckedOut();
            return ViewIfStudentLoggedIn();
        }

    }
}