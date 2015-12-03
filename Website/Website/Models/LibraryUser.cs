using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace Website.Models
{
    public class LibraryUser
    {
        public String emailId { get; set; }
        public String password { get; set; }
        
        public String Authenticate(String emailId,String password)
        {
            String userType = "";

            return userType;
        }

        public void AddStudent(String emailId, String password)
        {
            int count = 0;
            bool ValidEmailID = IsValid(emailId);
            if (ValidEmailID)
            {
                foreach (char c in emailId)
                {
                    if (c.Equals('@'))
                    {
                        if (emailId.Substring(count + 1).Equals("colorado.edu"))
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
            message.To.Add(emailId);
            message.Subject = "Acoount created";
            message.From = new MailAddress("nine8439@colorado.edu");
            message.Body = "Congratulations you have sucessfully registered";
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("yoursmtphost");
            smtp.Send(message);
        }

        public bool IsValid(string emailId)
        {
            try
            {
                MailAddress m = new MailAddress(emailId);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}