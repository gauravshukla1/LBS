using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            //Temporary redirect
            RedirectToAction("CheckOutBook");
            return View();
        }

        [HttpGet]
        public ActionResult Search()
        {
            Models.LibraryStudent student = new Models.LibraryStudent();
            //string term = "Water Resource";
            //List<List<String>> results = student.Search(term, "Title");
            //string term = "Johnson";
            //List<List<String>> results = student.Search(term, "Author");
            string term = "";
            List<List<String>> results = student.Search(term, "Category");
            ViewBag.Results = results;
            return View();
        }

        [HttpPost]
        public ActionResult Search(Models.LibraryStudent student)
        {
            string term = "9780784413531";
            List<List<String>> results = student.Search(term,"ISBN");
            return View();
        }

        [HttpGet]
        public ActionResult CheckOutBook()
        {
            Models.LibraryStudent student = new Models.LibraryStudent();
            //student.ISBN = "9780784413531";
            //student.EmailID = Convert.ToString(Session["EmailID"]);
            string msg = student.CheckOut("9780784413531", Convert.ToString(Session["EmailID"]));
            return View();
        }

        [HttpPost]
        public ActionResult CheckOutBook(Models.LibraryStudent student)
        {
            return View();
        }

        public ActionResult CheckedOutBook()
        {
            return View();
        }

    }
}