using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Website.Models
{
    /// <summary>
    /// Library Librarian Model. Inherits User Model
    /// This has methods AllCheckedOut and ReturnBook that connect to database and do these functions.
    /// </summary>
    public class LibraryLibrarian : LibraryUser
    {
        public LibraryLibrarian(String emailid) : base(emailid)
        {

        }

        public LibraryLibrarian() : base()
        {

        }

        public List<List<String>> AllCheckedOut()
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
            List<List<String>> array_list = new List<List<String>>();

            foreach (DataRow row in table.Rows)
            {
                List<String> temp = new List<String>();
                temp.Add(row["Id"].ToString().Trim());
                temp.Add(row["Email_ID"].ToString().Trim());
                temp.Add(row["ISBN"].ToString().Trim());
                temp.Add(row["Title"].ToString().Trim());
                temp.Add(row["Checkout_Date"].ToString().Trim());
                temp.Add(row["Due_Date"].ToString().Trim());
                array_list.Add(temp);
            }
            return array_list;
        }

        public String ReturnBook(String Id)
        {
            SqlConnection conn = null;
            conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.Librarian_ReturnBook", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = Id;
            cmd.ExecuteNonQuery();
            return "Return Successful";
        }
    }
}