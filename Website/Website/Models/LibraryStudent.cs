using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Website.Models
{
    public class LibraryStudent : LibraryUser
    {
        public LibraryStudent(String emailid) : base(emailid)
        {

        }

        public LibraryStudent() : base()
        {

        }

        public List<List<String>> AllCheckedOut()
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
            List<List<String>> array_list = new List<List<String>>();

            foreach (DataRow row in table.Rows)
            {
                List<String> temp = new List<String>();
                temp.Add(row["ISBN"].ToString());
                temp.Add(row["Title"].ToString());
                temp.Add(row["Checkout_Date"].ToString());
                temp.Add(row["Due_Date"].ToString());
                array_list.Add(temp);
            }
            return array_list;
        }

        public String CheckOut(String ISBN)
        {
            String msg = "Checkout Failure\n Please try again..!!";
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
                    cmd.ExecuteNonQuery();
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