using System;
using System.Net.Mail;
namespace Website.Models
{
    interface Email
    {
        void Email(String EmailID);
    }
    class SendEmail : Email
    {
        protected String _MailBody;
        public String MailBody
        {
            get { return _MailBody; }
            set { _MailBody = value; }
        }

        public SendEmail(String MailBody)
        {
            this.MailBody = MailBody;
        }

        public void Email(String EmailID)
        {
            MailMessage message = new MailMessage();
            message.To.Add(EmailID);
            message.Subject = "Acoount created";
            message.From = new MailAddress("LibraryManagementSystem@colorado.edu");
            message.Body = MailBody;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("SetThisLater");
            //smtp.Send(message);
        }
    }
    class AcceptStudentEmail : SendEmail
    {
        protected static String body = "Thank you for signing up. You can now access Library Management System.";
        public AcceptStudentEmail() : base(AcceptStudentEmail.body) { }
    }
    class RejectStudentEmail : SendEmail
    {
        protected static String body = "Your signup has been rejected. Kindly signup again at Library Management System.";
        public RejectStudentEmail(String Reason) : base(RejectStudentEmail.body + Reason) { }
    }
    class StudentCheckOutPass : SendEmail
    {
        protected static String body = "You have successfully checked out book with ISBN=";
        public StudentCheckOutPass(String ISBN) : base(StudentCheckOutPass.body + ISBN) { }
    }
    class StudentCheckOutFail : SendEmail
    {
        protected static String body = "You cannot checkout book with ISBN=";
        public StudentCheckOutFail(String ISBN, String Reason) : base(StudentCheckOutFail.body + ISBN + Reason) { }
    }
}