using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class Book
    {
        protected int _Id;
        protected String _ISBN;
        protected String _Title;
        protected String _Author;
        protected String _Category;
        protected String _Publisher;
        protected int _Year_Published;
        protected int _Quantity_Available;
        protected String _Location;
        
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public String ISBN
        {
            get { return _ISBN; }
            set { _ISBN = value; }
        }

        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public String Author
        {
            get { return _Author; }
            set { _Author = value; }
        }

        public String Category
        {
            get { return _Category; }
            set { _Category = value; }
        }

        public String Publisher
        {
            get { return _Publisher; }
            set { _Publisher = value; }
        }

        public int Year_Published
        {
            get { return _Year_Published; }
            set { _Year_Published = value; }
        }

        public int Quantity_Available
        {
            get { return _Quantity_Available; }
            set { _Quantity_Available = value; }
        }

        public String Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        public Book()
        {

        }

        public Book(String ISBN)
        {

        }

        public Book(int Id)
        {

        }

        public String AddBook(Book book)
        {
            SqlConnection conn = null;
            conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.Administrator_AddBook", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ISBN", SqlDbType.NVarChar).Value = book.ISBN;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = book.Title;
            cmd.Parameters.Add("@Author", SqlDbType.NVarChar).Value = book.Author;
            cmd.Parameters.Add("@Category", SqlDbType.NVarChar).Value = book.Category;
            cmd.Parameters.Add("@Publisher", SqlDbType.NVarChar).Value = book.Publisher;
            cmd.Parameters.Add("@Year_Published", SqlDbType.SmallInt).Value = book.Year_Published;
            cmd.Parameters.Add("@Quantity_Available", SqlDbType.Int).Value = book.Quantity_Available;
            cmd.Parameters.Add("@Location", SqlDbType.NVarChar).Value = book.Location;


            cmd.ExecuteNonQuery();
            return "Successfully added the book";
        }

        public String UpdateBook(Book book)
        {
            SqlConnection conn = null;
            conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.Administrator_UpdateBook", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = book.Id;
            cmd.Parameters.Add("@ISBN", SqlDbType.NVarChar).Value = book.ISBN;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = book.Title;
            cmd.Parameters.Add("@Author", SqlDbType.NVarChar).Value = book.Author;
            cmd.Parameters.Add("@Category", SqlDbType.NVarChar).Value = book.Category;
            cmd.Parameters.Add("@Publisher", SqlDbType.NVarChar).Value = book.Publisher;
            cmd.Parameters.Add("@Year_Published", SqlDbType.SmallInt).Value = book.Year_Published;
            cmd.Parameters.Add("@Quantity_Available", SqlDbType.Int).Value = book.Quantity_Available;
            cmd.Parameters.Add("@Location", SqlDbType.NVarChar).Value = book.Location;

            cmd.ExecuteNonQuery();
            return "Successfully Updated the book";
        }

        public String DeleteBook(Book book)
        {
            SqlConnection conn = null;
            conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.Administrator_DeleteBook", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = book.Id;

            cmd.ExecuteNonQuery();
            return "Successfully deleted the book";
        }

        public List<Book> AllBooks()
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
            List<Book> Book_Array = new List<Book>();

            foreach (DataRow row in table.Rows)
            {
                Book temp = new Book();
                temp.Id=Convert.ToInt32(row["Id"].ToString().Trim());
                temp.ISBN=(row["ISBN"].ToString().Trim());
                temp.Title=(row["Title"].ToString().Trim());
                temp.Author=(row["Author"].ToString().Trim());
                temp.Category=(row["Category"].ToString().Trim());
                temp.Quantity_Available=Convert.ToInt32(row["Quantity_Available"].ToString().Trim());
                temp.Location=(row["Location"].ToString().Trim());
                temp.Publisher=(row["Publisher"].ToString().Trim());
                temp.Year_Published=Convert.ToInt32(row["Year_Published"].ToString().Trim());
                Book_Array.Add(temp);
            }
            return Book_Array;
        }


    }
}