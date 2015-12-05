using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class LibraryStudent
    {
        private string _Title;
        private string _Author;
        private string _ISBN;
        private string _Query;
        private string _EmailID;

        public string Title
        {
            get { return _Title; }
            set { this._Title = value; }
        }

        public string Author
        {
            get { return _Author; }
            set { this._Author = value; }
        }

        public string ISBN
        {
            get { return _ISBN; }
            set { this._ISBN = value; }
        }

        public string Query
        {
            get { return _Query; }
            set { this._Query = value; }
        }

        public string EmailID
        {
            get { return _EmailID; }
            set { this._EmailID = value; }
        }

        public List<List<String>> Search(string term, string category)
        {
            if (category == "ISBN") return SearchISBN(term);
            if (category == "Title") return SearchTitle(term);
            if (category == "Author") return SearchAuthor(term);
            if (category == "Category") return SearchCategory(term);
            return new List<List<string>>();
        }

        private List<List<string>> SearchCategory(string term)
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
            List<List<string>> array_list = new List<List<string>>();

            foreach (DataRow row in table.Rows)
            {
                List<string> temp = new List<string>();
                temp.Add(row["ISBN"].ToString());
                temp.Add(row["Title"].ToString());
                temp.Add(row["Author"].ToString());
                temp.Add(row["Category"].ToString());
                temp.Add(row["Quantity_Available"].ToString());
                array_list.Add(temp);
            }
            return array_list;
        }

        private List<List<string>> SearchAuthor(string term)
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
            List<List<string>> array_list = new List<List<string>>();

            foreach (DataRow row in table.Rows)
            {
                List<string> temp = new List<string>();
                temp.Add(row["ISBN"].ToString());
                temp.Add(row["Title"].ToString());
                temp.Add(row["Author"].ToString());
                temp.Add(row["Category"].ToString());
                temp.Add(row["Quantity_Available"].ToString());
                array_list.Add(temp);
            }
            return array_list;
        }

        private List<List<string>> SearchTitle(string term)
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
            List<List<string>> array_list = new List<List<string>>();

            foreach (DataRow row in table.Rows)
            {
                List<string> temp = new List<string>();
                temp.Add(row["ISBN"].ToString());
                temp.Add(row["Title"].ToString());
                temp.Add(row["Author"].ToString());
                temp.Add(row["Category"].ToString());
                temp.Add(row["Quantity_Available"].ToString());
                array_list.Add(temp);
            }
            return array_list;
        }

        private List<List<string>> SearchISBN(string term)
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

            foreach(DataRow row in table.Rows)
            {
                List<string> temp = new List<string>();
                temp.Add(row["ISBN"].ToString());
                temp.Add(row["Title"].ToString());
                temp.Add(row["Author"].ToString());
                temp.Add(row["Category"].ToString());
                temp.Add(row["Quantity_Available"].ToString());
                array_list.Add(temp);
            }
            return array_list;
        }

        public string CheckOut(string ISBN,string emaildId)
        {
            string msg;
            
        }
    }
}