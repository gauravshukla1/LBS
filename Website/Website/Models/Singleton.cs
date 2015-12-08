using System;

namespace Website.Models
{
    public class Singleton
    {
        private static Singleton instance;
        private static Book _book;
        private static LibraryUser _user;
        private static LibraryStudent _student;
        private static LibraryLibrarian _librarian;
        private static LibraryAdministrator _administrator;
        private static String _MsgForLibrarian;

        public Book book
        {
            get { return _book; }
            set { _book = value; }
        }

        public LibraryUser user
        {
            get { return _user; }
            set { _user = value; }
        }

        public LibraryStudent student
        {
            get { return _student; }
            set { _student = value; }
        }

        public LibraryLibrarian librarian
        {
            get { return _librarian; }
            set { _librarian = value; }
        }

        public LibraryAdministrator administrator
        {
            get { return _administrator; }
            set { _administrator = value; }
        }

        public String MsgForLibrarian
        {
            get { return _MsgForLibrarian; }
            set { _MsgForLibrarian = value; }
        }

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                    _book = new Book();
                    _user = new LibraryUser();
                    _student = new LibraryStudent();
                    _librarian = new LibraryLibrarian();
                    _administrator = new LibraryAdministrator();
                }
                return instance;
            }
        }
    }
}