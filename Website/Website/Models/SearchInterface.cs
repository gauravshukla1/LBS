using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Website.Models
{
    interface Search
    {
        String term { get; set; }
        List<Book> Search();
    }

    class SearchByAuthor : Search
    {
        private String _term;

        public String term
        {
            get { return _term; }
            set { _term = value; }
        }

        public SearchByAuthor(String term)
        {
            _term = term;
        }

        public List<Book> Search()
        {
            SqlConnection con;
            DataTable table = new DataTable();
            con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            using (var cmd = new SqlCommand("SearchByAuthor", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@Author", SqlDbType.NVarChar).Value = term;
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }
            List<Book> Book_Array = new List<Book>();

            foreach (DataRow row in table.Rows)
            {
                Book temp = new Book();
                temp.Id = Convert.ToInt32(row["Id"].ToString().Trim());
                temp.ISBN = (row["ISBN"].ToString().Trim());
                temp.Title = (row["Title"].ToString().Trim());
                temp.Author = (row["Author"].ToString().Trim());
                temp.Category = (row["Category"].ToString().Trim());
                temp.Quantity_Available = Convert.ToInt32(row["Quantity_Available"].ToString().Trim());
                temp.Location = (row["Location"].ToString().Trim());
                temp.Publisher = (row["Publisher"].ToString().Trim());
                temp.Year_Published = Convert.ToInt32(row["Year_Published"].ToString().Trim());
                Book_Array.Add(temp);
            }
            return Book_Array;
        }
    }

    class SearchByISBN : Search
    {
        private String _term;

        public String term
        {
            get { return _term; }
            set { _term = value; }
        }

        public SearchByISBN(String term)
        {
            _term = term;
        }

        public List<Book> Search()
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
            List<Book> Book_Array = new List<Book>();

            foreach (DataRow row in table.Rows)
            {
                Book temp = new Book();
                temp.Id = Convert.ToInt32(row["Id"].ToString().Trim());
                temp.ISBN = (row["ISBN"].ToString().Trim());
                temp.Title = (row["Title"].ToString().Trim());
                temp.Author = (row["Author"].ToString().Trim());
                temp.Category = (row["Category"].ToString().Trim());
                temp.Quantity_Available = Convert.ToInt32(row["Quantity_Available"].ToString().Trim());
                temp.Location = (row["Location"].ToString().Trim());
                temp.Publisher = (row["Publisher"].ToString().Trim());
                temp.Year_Published = Convert.ToInt32(row["Year_Published"].ToString().Trim());
                Book_Array.Add(temp);
            }
            return Book_Array;
        }
    }

    class SearchByTitle : Search
    {
        private String _term;

        public String term
        {
            get { return _term; }
            set { _term = value; }
        }

        public SearchByTitle(String term)
        {
            _term = term;
        }

        public List<Book> Search()
        {
            SqlConnection con;
            DataTable table = new DataTable();
            con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            using (var cmd = new SqlCommand("SearchByTitle", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = term;
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }
            List<Book> Book_Array = new List<Book>();

            foreach (DataRow row in table.Rows)
            {
                Book temp = new Book();
                temp.Id = Convert.ToInt32(row["Id"].ToString().Trim());
                temp.ISBN = (row["ISBN"].ToString().Trim());
                temp.Title = (row["Title"].ToString().Trim());
                temp.Author = (row["Author"].ToString().Trim());
                temp.Category = (row["Category"].ToString().Trim());
                temp.Quantity_Available = Convert.ToInt32(row["Quantity_Available"].ToString().Trim());
                temp.Location = (row["Location"].ToString().Trim());
                temp.Publisher = (row["Publisher"].ToString().Trim());
                temp.Year_Published = Convert.ToInt32(row["Year_Published"].ToString().Trim());
                Book_Array.Add(temp);
            }
            return Book_Array;
        }
    }

    class SearchByCategory : Search
    {
        private String _term;

        public String term
        {
            get { return _term; }
            set { _term = value; }
        }

        public SearchByCategory(String term)
        {
            _term = term;
        }

        public List<Book> Search()
        {
            SqlConnection con;
            DataTable table = new DataTable();
            con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            using (var cmd = new SqlCommand("SearchByCategory", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@Category", SqlDbType.NVarChar).Value = term;
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }
            List<Book> Book_Array = new List<Book>();

            foreach (DataRow row in table.Rows)
            {
                Book temp = new Book();
                temp.Id = Convert.ToInt32(row["Id"].ToString().Trim());
                temp.ISBN = (row["ISBN"].ToString().Trim());
                temp.Title = (row["Title"].ToString().Trim());
                temp.Author = (row["Author"].ToString().Trim());
                temp.Category = (row["Category"].ToString().Trim());
                temp.Quantity_Available = Convert.ToInt32(row["Quantity_Available"].ToString().Trim());
                temp.Location = (row["Location"].ToString().Trim());
                temp.Publisher = (row["Publisher"].ToString().Trim());
                temp.Year_Published = Convert.ToInt32(row["Year_Published"].ToString().Trim());
                Book_Array.Add(temp);
            }
            return Book_Array;
        }
    }
}
