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
            if(IsValid(user.EmailID,user.Password))                            
            {
                FormsAuthentication.SetAuthCookie(user.EmailID, false);
                /*ViewBag.Message = login_type;
                return View();*/
                Session["login_type"] = login_type;
                Session["EmailID"] = user.EmailID;
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
            if (StoreUser(user.EmailID, user.Password,user.FirstName,user.LastName))
            {
                return RedirectToAction("Index","User");
            }
            else
            {
                ModelState.AddModelError("", "Registration Data is Incorrect");
            }
            return View();
        }
        private bool StoreUser(String EmailID, String Password, String FirstName, String LastName)
        {
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {
                if (EmailID != null && Password != null && FirstName != null && LastName != null)
                {
                    conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("dbo.User_AddStudent", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add the input parameter and set its properties.
                    SqlParameter parameter1 = new SqlParameter();
                    parameter1.ParameterName = "@email_id";
                    parameter1.SqlDbType = SqlDbType.NVarChar;
                    parameter1.Direction = ParameterDirection.Input;
                    parameter1.Value = EmailID;

                    // Add the parameter to the Parameters collection. 
                    cmd.Parameters.Add(parameter1);

                    // Add the input parameter and set its properties.
                    SqlParameter parameter2 = new SqlParameter();
                    parameter2.ParameterName = "@Password";
                    parameter2.SqlDbType = SqlDbType.NVarChar;
                    parameter2.Direction = ParameterDirection.Input;
                    parameter2.Value = Password;

                    // Add the parameter to the Parameters collection. 
                    cmd.Parameters.Add(parameter2);

                    // Add the input parameter and set its properties.
                    SqlParameter parameter3 = new SqlParameter();
                    parameter3.ParameterName = "@FirstName";
                    parameter3.SqlDbType = SqlDbType.NVarChar;
                    parameter3.Direction = ParameterDirection.Input;
                    parameter3.Value = FirstName;

                    // Add the parameter to the Parameters collection. 
                    cmd.Parameters.Add(parameter3);

                    // Add the input parameter and set its properties.
                    SqlParameter parameter4 = new SqlParameter();
                    parameter4.ParameterName = "@LastName";
                    parameter4.SqlDbType = SqlDbType.NVarChar;
                    parameter4.Direction = ParameterDirection.Input;
                    parameter4.Value = LastName;

                    // Add the parameter to the Parameters collection. 
                    cmd.Parameters.Add(parameter4);

                    cmd.ExecuteNonQuery();

                    return true;
                }
                else
                    return false;
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

            
  
            
        }
        private bool IsValid(String EmailID, String Password)
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
                parameter1.Value = EmailID;

                // Add the parameter to the Parameters collection. 
                cmd.Parameters.Add(parameter1);

                // Add the input parameter and set its properties.
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@Password";
                parameter.SqlDbType = SqlDbType.NVarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = Password;

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