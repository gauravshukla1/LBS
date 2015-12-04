using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Website.Controllers
{
    public class UserController : Controller
    {
        private string login_type;
        // _connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True";
        // GET: User
        public ActionResult Index()
        {
            login_type = Convert.ToString(Session["login_type"]).Trim();

            if (login_type != null) {
                if (login_type == "Student") { return RedirectToAction("Index","Student"); }
                if (login_type == "Librarian") { return RedirectToAction("Index", "Librarian"); }
                if (login_type == "Admin") { return RedirectToAction("Index", "Administrator"); }
            }
            return View();
            
        }

        [HttpGet]
        public ActionResult LogIn()
        {

            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Models.LibraryUser user)
        {
            if(IsValid(user.emailId,user.password))                            
            {
                FormsAuthentication.SetAuthCookie(user.emailId, false);
                /*ViewBag.Message = login_type;
                return View();*/
                Session["login_type"] = login_type;
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Log In Data is Incorrect");
            }
            return View();
        }

        
        public ActionResult LogOut()
        {

            return View();
        }


        [HttpGet]
        public ActionResult Registration()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Registration(Models.LibraryUser user)
        {
            
            return View();
        }

        private bool IsValid(String emailId, String password)
        {
            //return true;
            SqlConnection conn = null;
            SqlDataReader rdr = null;
           
            try
            {
                conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.User_Authenticate", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add the input parameter and set its properties.
                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@email_id";
                parameter1.SqlDbType = SqlDbType.NVarChar;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = emailId;

                // Add the parameter to the Parameters collection. 
                cmd.Parameters.Add(parameter1);

                // Add the input parameter and set its properties.
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@password";
                parameter.SqlDbType = SqlDbType.NVarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = password;

                // Add the parameter to the Parameters collection. 
                cmd.Parameters.Add(parameter);

                // Add the input parameter and set its properties.
                SqlParameter outparameter = new SqlParameter();
                outparameter.ParameterName = "@login_type";
                outparameter.SqlDbType = SqlDbType.NVarChar;
                outparameter.Size = 12;
                outparameter.Direction = ParameterDirection.Output;
                

                // Add the parameter to the Parameters collection. 
                cmd.Parameters.Add(outparameter);

                cmd.ExecuteNonQuery();
                login_type = Convert.ToString(cmd.Parameters["@login_type"].Value);
                //login_type = login_type;
                /*while (rdr.Read())
                {
                    Console.WriteLine(
                        "Product: {0,-25} Price: ${1,6:####.00}",
                        rdr["TenMostExpensiveProducts"],
                        rdr["UnitPrice"]);
                }*/
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
            if (login_type != null)
                return true;
            else
                return false;
        }
    }
}