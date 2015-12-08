using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Website.Models
{
    public class LibraryAdministrator : LibraryUser
    {
        public LibraryAdministrator(String emailid) : base(emailid)
        {

        }

        public LibraryAdministrator() : base()
        {

        }

        public String AddLibrarian(LibraryLibrarian librarian)
        {
            if (!librarian.IsValid(EmailID)) { return "Not valid Email ID"; }
            try { 
            SqlConnection conn = null;

            conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.Administrator_AddLibrarian", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@email_id", SqlDbType.NVarChar).Value = librarian.EmailID;
            cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = librarian.Password;
            cmd.Parameters.Add("@firstname", SqlDbType.NVarChar).Value = librarian.FirstName;
            cmd.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = librarian.LastName;

            cmd.ExecuteNonQuery();
            return "Successfully added the librarian.";
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Message.StartsWith("Violation of UNIQUE KEY constraint"))
                {
                    return "Duplicate Email_ID. Check again.";
                }
                else
                    throw;
            }
        }

        public String UpdateLibrarian(LibraryLibrarian librarian)
        {
            if (!librarian.IsValid(EmailID)) { return "Not valid Email ID"; }
            try { 
                SqlConnection conn = null;

                conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.Administrator_UpdateLibrarian", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = librarian.Id;
                cmd.Parameters.Add("@Email_ID", SqlDbType.NVarChar).Value = librarian.EmailID;
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = librarian.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = librarian.LastName;

                cmd.ExecuteNonQuery();
                return "Successfully updated the librarian. Password has been reset to 1234.";
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Message.StartsWith("Violation of UNIQUE KEY constraint"))
                {
                    return "Duplicate Email_ID. Check again.";
                }
                else
                    throw;
            }
        }

        public String DeleteLibrarian(LibraryLibrarian librarian)
        {
            if (!librarian.IsValid(EmailID)) { return "Not valid Email ID"; }

            SqlConnection conn = null;

            conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.Administrator_DeleteLibrarian", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = librarian.Id;

            cmd.ExecuteNonQuery();
            return "Successfully Deleted the librarian.";
        }

        public List<LibraryLibrarian> AllLibrarians()
        {
            //Displaying all books in the library for Admin to update or delete accordingly
            SqlConnection con;
            DataTable table = new DataTable();
            con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LMS_DB.mdf;Integrated Security = True");
            using (var cmd = new SqlCommand("Administrator_AllLibrarians", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }
            List<LibraryLibrarian> Librarian_List = new List<LibraryLibrarian>();

            foreach (DataRow row in table.Rows)
            {
                LibraryLibrarian librarian = new LibraryLibrarian();
                librarian.Id = Convert.ToInt32(row["ID"].ToString().Trim());
                librarian.FirstName = (row["FirstName"].ToString().Trim());
                librarian.LastName = (row["LastName"].ToString().Trim());
                librarian.EmailID = (row["Email_ID"].ToString().Trim());
                librarian.Password = (row["Password"].ToString().Trim());
                Librarian_List.Add(librarian);
            }
            return Librarian_List;
        }
    }
}