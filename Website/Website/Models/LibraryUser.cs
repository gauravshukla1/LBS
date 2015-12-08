using System;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    /// <summary>
    /// Basic User Model as used in database user_credentials table.
    /// There is a blank constructor for use with web forms
    /// The other constructor is used to populate other variables from the database by sending email_id
    /// It has methods or Authentication and Adding a new user.
    /// </summary>
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
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [Required(ErrorMessage = "Email ID field cannot be kept blank")]
        [EmailAddress]
        [StringLength(35, MinimumLength = 14)]
        public String EmailID
        {
            get { return _EmailID; }
            set { _EmailID = value; }
        }

        [Required(ErrorMessage = "Password field cannot be kept blank")]
        [DataType(DataType.Password)]
        [StringLength(25, MinimumLength = 3)]
        public String Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        [Required(ErrorMessage = "First Name field cannot be kept blank")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        [Required(ErrorMessage = "Last Name field cannot be kept blank")]
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
                this.FirstName = (row["FirstName"].ToString().Trim());
                this.LastName = (row["LastName"].ToString().Trim());
                this.EmailID = (row["Email_ID"].ToString().Trim());
                this.Password = (row["Password"].ToString().Trim());
                this.login_type = (row["Login_Type"].ToString().Trim());
                this.Books_Allowed = Convert.ToInt32(row["Books_Allowed"].ToString().Trim());
                this.Books_Borrowed = Convert.ToInt32(row["Books_Borrowed"].ToString().Trim());
            }
        }

        public bool IsValid(String EmailID)
        {
            try
            {
                MailAddress m = new MailAddress(EmailID);
                if (EmailID.Contains("@colorado.edu") && EmailID.Substring(EmailID.Length - 13) == "@colorado.edu")
                {
                    return true;
                }
            }
            catch (FormatException)
            {
                return false;
            }
            return false;
        }

        public bool Authenticate(LibraryUser user)
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

                cmd.Parameters.Add("@email_id", SqlDbType.NVarChar).Value = user.EmailID;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = user.Password;

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

        public bool AddUser(LibraryUser user)
        {
            if (!IsValid(user.EmailID)) { return false; }

            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {
                if (user.EmailID != null && user.Password != null && user.FirstName != null && user.LastName != null)
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
                    parameter1.Value = user.EmailID;

                    // Add the parameter to the Parameters collection. 
                    cmd.Parameters.Add(parameter1);

                    // Add the input parameter and set its properties.
                    SqlParameter parameter2 = new SqlParameter();
                    parameter2.ParameterName = "@password";
                    parameter2.SqlDbType = SqlDbType.NVarChar;
                    parameter2.Direction = ParameterDirection.Input;
                    parameter2.Value = user.Password;

                    // Add the parameter to the Parameters collection. 
                    cmd.Parameters.Add(parameter2);

                    // Add the input parameter and set its properties.
                    SqlParameter parameter3 = new SqlParameter();
                    parameter3.ParameterName = "@firstname";
                    parameter3.SqlDbType = SqlDbType.NVarChar;
                    parameter3.Direction = ParameterDirection.Input;
                    parameter3.Value = user.FirstName;

                    // Add the parameter to the Parameters collection. 
                    cmd.Parameters.Add(parameter3);

                    // Add the input parameter and set its properties.
                    SqlParameter parameter4 = new SqlParameter();
                    parameter4.ParameterName = "@lastname";
                    parameter4.SqlDbType = SqlDbType.NVarChar;
                    parameter4.Direction = ParameterDirection.Input;
                    parameter4.Value = user.LastName;

                    // Add the parameter to the Parameters collection. 
                    cmd.Parameters.Add(parameter4);

                    cmd.ExecuteNonQuery();

                    return true;
                }
                else
                    return false;
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Message.StartsWith("Violation of UNIQUE KEY constraint"))
                {
                    return false;
                }
                else
                    throw;
            }
            catch (Exception)
            {
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