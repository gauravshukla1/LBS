using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library_Management_System.Models;

namespace Library_Management_System.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            List<IndexModel> newList = new List<IndexModel>();

            IndexModel newIndexModel1 = new IndexModel
            {
                ID = 1,
                Description = "First Item",
                Comments = "This is the first Instance of the Model"
            };

            IndexModel newIndexModel2 = new IndexModel
            {
                ID = 2,
                Description = "Second Item",
                Comments = "This is the second Instance of the Model"
            };

            newList.Add(newIndexModel1);
            newList.Add(newIndexModel2);

            return View(newList);
        }
    }
}