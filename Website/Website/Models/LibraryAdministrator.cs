using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class LibraryAdministrator : LibraryUser
    {
        public LibraryAdministrator(String emailid) : base(emailid)
        {

        }
        public LibraryAdministrator()
        {

        }

        protected string _ISBN;
            protected string _Title;
            protected string _Author;
            protected string _Category;
            protected string _Publisher;
            protected int _Year_Published;
            protected int _Quantity;
            protected string _Location;

            public string ISBN
            {
                get { return _ISBN; }
                set { _ISBN = value; }
            }

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public string Author
        {
            get { return _Author; }
            set { _Author = value; }
        }

        public string Category
        {
                get { return _Category; }
                set { _Category = value; }
            }

        public string Publisher
        {
            get { return _Publisher; }
            set { _Publisher = value; }
        }

            public int Year_Published
        {
                get { return _Year_Published; }
                set { _Year_Published = value; }
            }

            public int Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; }
            }

        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        public string AddBook()
        {
            SqlConnection conn = null;

            conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.Administrator_AddBook", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.Add("@email_id", SqlDbType.NVarChar).Value = emailId;
            cmd.Parameters.Add("@ISBN", SqlDbType.NVarChar).Value = ISBN;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = Title;
            cmd.Parameters.Add("@Author", SqlDbType.NVarChar).Value = Author;
            cmd.Parameters.Add("@Category", SqlDbType.NVarChar).Value = Category;
            cmd.Parameters.Add("@Publisher", SqlDbType.NVarChar).Value = Publisher;
            cmd.Parameters.Add("@Year_Published", SqlDbType.SmallInt).Value = Year_Published;
            cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Quantity;
            cmd.Parameters.Add("@Location", SqlDbType.NVarChar).Value = Location;

            cmd.ExecuteNonQuery();
            return "Add Book Successful";
        }
    }
}