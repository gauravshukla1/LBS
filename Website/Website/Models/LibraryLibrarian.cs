using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class LibraryLibrarian : LibraryUser
    {
        public LibraryLibrarian(String emailid) : base(emailid)
        {

        }

        public List<List<string>> AllCheckedOut()
        {
            SqlConnection con;
            DataTable table = new DataTable();
            con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            using (var cmd = new SqlCommand("Librarian_AllCheckedOut", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }
            List<List<string>> array_list = new List<List<string>>();

            foreach (DataRow row in table.Rows)
            {
                List<string> temp = new List<string>();
                temp.Add(row["Email_ID"].ToString());
                temp.Add(row["ISBN"].ToString());
                temp.Add(row["Title"].ToString());
                temp.Add(row["Checkout_Date"].ToString());
                temp.Add(row["Due_Date"].ToString());
                array_list.Add(temp);
            }
            return array_list;
        }

        public string ReturnBook(string ISBN, string emailId)
        {
            SqlConnection conn = null;

                conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.Librarian_ReturnBook", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@email_id", SqlDbType.NVarChar).Value = emailId;
                    cmd.Parameters.Add("@ISBN", SqlDbType.NVarChar).Value = ISBN;

                    cmd.ExecuteNonQuery(); 
                    return "Return Successful";
        }
    }
}