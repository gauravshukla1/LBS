using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace Website.Models
{
    public class LibraryUser
    {
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string login_type { get; set; }

        public String Authenticate(String EmailID,String Password)
        {
            String userType = "";

            return userType;
        }

        public void AddStudent(String EmailID, String Password)
        {
            int count = 0;
            bool ValidEmailID = IsValid(EmailID);
            if (ValidEmailID)
            {
                foreach (char c in EmailID)
                {
                    if (c.Equals('@'))
                    {
                        if (EmailID.Substring(count + 1).Equals("colorado.edu"))
                        {  //Accept Email
                            AcceptEmail();
                            break;
                        }
                        else
                            //Reject email does not make sense
                            break;
                    }
                    count++;
                }
                        
            }

        }

        private void AcceptEmail()
        {
            MailMessage message = new MailMessage();
            message.To.Add(EmailID);
            message.Subject = "Acoount created";
            message.From = new MailAddress("nine8439@colorado.edu");
            message.Body = "Congratulations you have sucessfully registered";
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("yoursmtphost");
            smtp.Send(message);
        }

        public bool IsValid(string EmailID)
        {
            try
            {
                MailAddress m = new MailAddress(EmailID);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}