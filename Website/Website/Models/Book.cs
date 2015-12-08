using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Website.Models
{
    /// <summary>
    /// Basic book model as used in Database in the inventory table.
    /// This has methods AddBook, UpdateBook and DeleteBook that connect to database and do these functions.
    /// The all book method makes use of the search interface and returns all book.
    /// The Search method makes use of the Search interface.
    /// </summary>
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
            return Search("","");
        }

        public List<Book> Search(String term,String criteria)
        {
            Website.Models.Search search;
            switch (criteria)
            {
                case "Author": search = new Models.SearchByAuthor(); break;
                case "ISBN": search = new Models.SearchByISBN(); break;
                case "Category": search = new Models.SearchByCategory(); break;
                default: search = new Models.SearchByTitle(); break;
            }
            return search.Search(term);
        }
    }
}