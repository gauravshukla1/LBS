using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Search Functionality is Implemented using a Strategy design pattern.
/// </summary>
namespace Website.Models
{
    /// <summary>
    /// Search interface just declares signature methods --> Abstraction
    /// </summary>
    interface Search
    {
        List<Book> Search(String term);        /// This method is to be concretely defined in Search Implementations
    }
    /// <summary>Concrete Implementation to Search w.r.t criteria.</summary>
    /// <info>Instances are initialized in the controller. This passes the dependency from Book Model to controller</info>
    class SearchService : Search
    {
        protected String _criteria;
        public String criteria
        {
            get { return _criteria; }
            set { _criteria = value; }
        }

        public SearchService(String criteria)
        {
            this.criteria = criteria;
        }

        public List<Book> Search(String term)
        {
            SqlConnection con;
            DataTable table = new DataTable();
            con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            using (var cmd = new SqlCommand("SearchBy" + criteria, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@" + criteria, SqlDbType.NVarChar).Value = term;
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
    /// <summary>Extended to Search w.r.t ISBN.</summary>
    class SearchByISBN : SearchService
    {
        public SearchByISBN() : base("ISBN")
        {
        }
    }
    /// <summary>Extended to Search w.r.t Title.</summary>
    class SearchByTitle : SearchService
    {
        public SearchByTitle() : base("Title")
        {
        }
    }
    /// <summary>Extended to Search w.r.t Category.</summary>
    class SearchByCategory : SearchService
    {
        public SearchByCategory() : base("Category")
        {
        }
    }
    /// <summary>Extended Search w.r.t Author.</summary>
    class SearchByAuthor : SearchService
    {
        public SearchByAuthor() : base("Author")
        {
        }
    }
}