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

            cmd.Parameters.Add("@ISBN", SqlDbType.NVarChar).Value = ISBN;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = Title;
            cmd.Parameters.Add("@Author", SqlDbType.NVarChar).Value = Author;
            cmd.Parameters.Add("@Category", SqlDbType.NVarChar).Value = Category;
            cmd.Parameters.Add("@Publisher", SqlDbType.NVarChar).Value = Publisher;
            cmd.Parameters.Add("@Year_Published", SqlDbType.SmallInt).Value = Year_Published;
            cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Quantity;
            cmd.Parameters.Add("@Location", SqlDbType.NVarChar).Value = Location;

            cmd.ExecuteNonQuery();
            return "Successfully added the book";
        }

        public string AddLibrarian()
        {
            Models.LibraryUser lib_user = new Models.LibraryUser();
            if (! lib_user.IsValid(EmailID)) { return "Not valid Email ID"; }

            SqlConnection conn = null;

            conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.Administrator_AddLibrarian", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("@email_id", SqlDbType.NVarChar).Value = EmailID;
            cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = Password;
            cmd.Parameters.Add("@firstname", SqlDbType.NVarChar).Value = FirstName;
            cmd.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = LastName;

            cmd.ExecuteNonQuery();
            return "Successfully added the librarian";
        }

        public List<List<string>> AllBooks()
        {
            //Displaying all books in the library for Admin to update or delete accordingly
            SqlConnection con;
            DataTable table = new DataTable();
            con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            using (var cmd = new SqlCommand("SearchByTitle", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = "";
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }
            List<List<string>> array_list = new List<List<string>>();

            foreach (DataRow row in table.Rows)
            {
                List<string> temp = new List<string>();
                temp.Add(row["ISBN"].ToString());
                temp.Add(row["Title"].ToString());
                temp.Add(row["Author"].ToString());
                temp.Add(row["Category"].ToString());
                temp.Add(row["Quantity_Available"].ToString());
                temp.Add(row["Location"].ToString());
                temp.Add(row["Publisher"].ToString());
                temp.Add(row["Year_Published"].ToString());
                temp.Add(row["Id"].ToString());
                array_list.Add(temp);
            }
            return array_list;
        }

        public string UpdateBook(string ISBN,int Qty,string Loc)
        {
            SqlConnection conn = null;

            conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.Administrator_UpdateBook", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("@ISBN", SqlDbType.NVarChar).Value = ISBN;
            cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value =  Qty;
            cmd.Parameters.Add("@Location", SqlDbType.NVarChar).Value = Loc;

            cmd.ExecuteNonQuery();
            return "Successfully updated the book";
        }

        public List<List<string>> SearchISBN(string term)
        {
            SqlConnection con;
            DataTable table = new DataTable();
            con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            using (var cmd = new SqlCommand("SearchByISBN", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@ISBN", SqlDbType.NVarChar).Value = term;
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }
            List<List<string>> array_list = new List<List<string>>();

            foreach (DataRow row in table.Rows)
            {
                List<string> temp = new List<string>();
                temp.Add(row["ISBN"].ToString());
                temp.Add(row["Title"].ToString());
                temp.Add(row["Author"].ToString());
                temp.Add(row["Quantity_Available"].ToString());
                temp.Add(row["Location"].ToString());
                
                array_list.Add(temp);
            }
            return array_list;
        }
    }
}