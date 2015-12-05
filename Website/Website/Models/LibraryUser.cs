using System;
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
        private string _EmailID;
        private string _Password;
        private string _FirstName;
        private string _LastName;
        public string _login_type;

        public string EmailID
        {
            get { return _EmailID; }
            set { this._EmailID = value; } 
        }

        public string Password
        {
            get { return _Password; }
            set { this._Password = value; }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set { this._FirstName = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { this._LastName = value; }
        }

        public string login_type
        {
            get { return _login_type; }
            set { this._login_type = value; }
        }
        public String Authenticate(String EmailID,String Password)
        {
            String userType = "";

            return userType;
        }

        public void AddStudent(String EmailID, String Password)
        {
            int count = 0;
            bool ValidEmailID = IsValid(EmailID);
            if (ValidEmailID)
            {
                foreach (char c in EmailID)
                {
                    if (c.Equals('@'))
                    {
                        if (EmailID.Substring(count + 1).Equals("colorado.edu"))
                        {  //Accept Email
                            AcceptEmail();
                            break;
                        }
                        else
                            //Reject email does not make sense
                            break;
                    }
                    count++;
                }
                        
            }

        }

        private void AcceptEmail()
        {
            MailMessage message = new MailMessage();
            message.To.Add(EmailID);
            message.Subject = "Acoount created";
            message.From = new MailAddress("nine8439@colorado.edu");
            message.Body = "Congratulations you have sucessfully registered";
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("yoursmtphost");
            smtp.Send(message);
        }

        public bool IsValid(string EmailID)
        {
            try
            {
                MailAddress m = new MailAddress(EmailID);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool IsValid(String EmailID, String Password)
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
                login_type = Convert.ToString(cmd.Parameters["@login_type"].Value);
                
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
            if (login_type.Trim() == "Student" || login_type.Trim() == "Librarian" || login_type.Trim() == "Admin")
                return true;
            else
                return false;
        }

        public bool StoreUser(String EmailID, String Password, String FirstName, String LastName)
        {
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {
                if (EmailID != null && Password != null && FirstName != null && LastName != null)
                {
                    // conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["_connectionstring"].ConnectionString);
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