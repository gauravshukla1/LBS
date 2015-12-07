﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;

namespace Website.Models
{
    public class LibraryUser
    {
        protected int _Id;
        protected String _EmailID;
        protected String _Password;
        protected String _FirstName;
        protected String _LastName;
        protected String _login_type;
        protected int _Books_Allowed;
        protected int _Books_Borrowed;

        public int Id {
            get { return _Id; }
            set { _Id = value; }
        }

        public String EmailID
        {
            get { return _EmailID; }
            set { _EmailID = value; }
        }

        public String Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public String login_type
        {
            get { return _login_type; }
            set { _login_type = value; }
        }

        public int Books_Allowed
        {
            get { return _Books_Allowed; }
            set { _Books_Allowed = value; }
        }

        public int Books_Borrowed
        {
            get { return _Books_Borrowed; }
            set { _Books_Borrowed = value; }
        }

        public LibraryUser()
        {
        }

        public LibraryUser(String emailid)
        {
            SqlConnection con;
            DataTable table = new DataTable();
            con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            using (var cmd = new SqlCommand("User_GetUser", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@email_id", SqlDbType.NVarChar).Value = emailid;
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }

            foreach (DataRow row in table.Rows)
            {
                this.FirstName=(row["FirstName"].ToString().Trim());
                this.LastName = (row["LastName"].ToString().Trim());
                this.EmailID = (row["Email_ID"].ToString().Trim());
                this.Password = (row["Password"].ToString().Trim());
                this.login_type = (row["Login_Type"].ToString().Trim());
                this.Books_Allowed = Convert.ToInt32(row["Books_Allowed"].ToString().Trim());
                this.Books_Borrowed = Convert.ToInt32(row["Books_Borrowed"].ToString().Trim());
            }
        }

        private void AcceptEmail()
        {
            MailMessage message = new MailMessage();
            message.To.Add(EmailID);
            message.Subject = "Acoount created";
            message.From = new MailAddress("nine8439@colorado.edu");
            message.Body = "Congratulations you have sucessfully registered";
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("SetThisLater");
            //smtp.Send(message);
        }

        public bool IsValid(String EmailID)
        {
            try
            {
                MailAddress m = new MailAddress(EmailID);
                if (EmailID.Contains("@colorado.edu"))
                {
                    //AcceptEmail();
                    return true;
                }
            }
            catch (FormatException)
            {
                return false;
            }
            return false;
        }

        public bool Authenticate(String EmailID, String Password)
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

                cmd.Parameters.Add("@email_id", SqlDbType.NVarChar).Value = EmailID;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = Password;

                // Add the output parameter and set its properties.
                SqlParameter outparameter = new SqlParameter();
                outparameter.ParameterName = "@login_type";
                outparameter.SqlDbType = SqlDbType.NVarChar;
                outparameter.Size = 12;
                outparameter.Direction = ParameterDirection.Output;


                // Add the parameter to the Parameters collection. 
                cmd.Parameters.Add(outparameter);

                cmd.ExecuteNonQuery();
                login_type = Convert.ToString(cmd.Parameters["@login_type"].Value).Trim();
                
            }
            catch (Exception)
            {

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
            if (login_type == "Student" || login_type == "Librarian" || login_type == "Admin")
                return true;
            else
                return false;
        }

        public bool AddUser(String EmailID, String Password, String FirstName, String LastName)
        {
            if (!IsValid(EmailID)) { return false; }

            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {
                if (EmailID != null && Password != null && FirstName != null && LastName != null)
                {
                    // conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["_connectionString"].ConnectionString);
                    conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("dbo.User_AddStudent", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // cmd.Parameters.Add("@email_id", SqlDbType.NVarChar).Value = EmailID;
                    // cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = Password;
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
                    parameter2.ParameterName = "@password";
                    parameter2.SqlDbType = SqlDbType.NVarChar;
                    parameter2.Direction = ParameterDirection.Input;
                    parameter2.Value = Password;

                    // Add the parameter to the Parameters collection. 
                    cmd.Parameters.Add(parameter2);

                    // Add the input parameter and set its properties.
                    SqlParameter parameter3 = new SqlParameter();
                    parameter3.ParameterName = "@firstname";
                    parameter3.SqlDbType = SqlDbType.NVarChar;
                    parameter3.Direction = ParameterDirection.Input;
                    parameter3.Value = FirstName;

                    // Add the parameter to the Parameters collection. 
                    cmd.Parameters.Add(parameter3);

                    // Add the input parameter and set its properties.
                    SqlParameter parameter4 = new SqlParameter();
                    parameter4.ParameterName = "@lastname";
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
    }
}