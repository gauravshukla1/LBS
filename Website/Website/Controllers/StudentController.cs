using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult ViewIfStudentLoggedIn()
        {
            string login_type = Convert.ToString(Session["login_type"]);
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
            Models.LibraryStudent student = new Models.LibraryStudent(Convert.ToString(Session["EmailID"]));
            ViewBag.Message = student.FirstName+" "+student.LastName;
            return ViewIfStudentLoggedIn();
        }

        [HttpGet]
        public ActionResult Search()
        {
            Models.LibraryStudent student = new Models.LibraryStudent(Convert.ToString(Session["EmailID"]));
            ViewBag.Results = student.Search(Request.QueryString["Term"], Request.QueryString["Criteria"]);
            return ViewIfStudentLoggedIn();
        }

        [HttpGet]
        public ActionResult CheckOut()
        {
            Models.LibraryStudent student = new Models.LibraryStudent(Convert.ToString(Session["EmailID"]));
            Session["Message"] = student.CheckOut(Request.QueryString["ISBN"]);
            return RedirectToAction("AllCheckedOut", "Student");
        }

        public ActionResult AllCheckedOut()
        {
            Models.LibraryStudent student = new Models.LibraryStudent(Convert.ToString(Session["EmailID"]));
            ViewBag.Results = student.AllCheckedOut();
            return ViewIfStudentLoggedIn();
        }

    }
}