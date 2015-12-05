using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class LibraryAdministrator : LibraryUser
    {
        public LibraryAdministrator(String emailid) : base(emailid)
        {

        }
    }
}