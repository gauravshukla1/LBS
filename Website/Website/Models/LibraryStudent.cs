using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class LibraryStudent : LibraryUser
    {
        public LibraryStudent(String emailid) : base(emailid)
        {

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

        public List<List<string>> AllCheckedOut()
        {
            SqlConnection con;
            DataTable table = new DataTable();
            con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            using (var cmd = new SqlCommand("Student_AllCheckedOut", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@email_id", SqlDbType.NVarChar).Value = this.EmailID;
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }
            List<List<string>> array_list = new List<List<string>>();

            foreach (DataRow row in table.Rows)
            {
                List<string> temp = new List<string>();
                temp.Add(row["ISBN"].ToString());
                temp.Add(row["Title"].ToString());
                temp.Add(row["Checkout_Date"].ToString());
                temp.Add(row["Due_Date"].ToString());
                array_list.Add(temp);
            }
            return array_list;
        }

        public string CheckOut(string ISBN)
        {
            string msg = "Checkout Failure\n Please try again..!!";
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {
                conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.Librarian_CheckBorrowable", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@email_id", SqlDbType.NVarChar).Value = this.EmailID;
                // Add the output parameter and set its properties.
                SqlParameter outparameter = new SqlParameter();
                outparameter.ParameterName = "@Number";
                outparameter.SqlDbType = SqlDbType.Int;
                //outparameter.Size = 12;
                outparameter.Direction = ParameterDirection.Output;


                // Add the parameter to the Parameters collection. 
                cmd.Parameters.Add(outparameter);

                cmd.ExecuteNonQuery();
                int returnValue = Convert.ToInt32(cmd.Parameters["@Number"].Value);

                if (returnValue == 0)
                    return "Cannot borrow any more books";
                else
                {
                    cmd = new SqlCommand("dbo.Student_Checkout", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@email_id", SqlDbType.NVarChar).Value = this.EmailID;
                    cmd.Parameters.Add("@ISBN", SqlDbType.NVarChar).Value = ISBN;

                    SqlParameter retValue = cmd.Parameters.Add("return", SqlDbType.Int);
                    retValue.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery(); // MISSING
                    int returnvalue = (int)retValue.Value;

                    if (returnvalue == 0)
                        return "CheckOut Successful";

                }
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
            return msg;
        }
    }
}