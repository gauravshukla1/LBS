using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class LibraryUser
    {
        private String emailId { get; set; }
        private String password { get; set; }
        
        public String Authenticate(String emailId,String password)
        {
            String userType = "";

            return userType;
        }
    }
}