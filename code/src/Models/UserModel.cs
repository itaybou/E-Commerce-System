using System;

namespace ECommerceSystem.Models
{
    public class UserModel
    {
        private Guid _id;
        private string _username;
        private string _fname;
        private string _lname;
        private string _email;

        public UserModel(Guid id, string username, string fname, string lname, string email)
        {
            _id = id;
            _username = username;
            _fname = fname;
            _lname = lname;
            _email = email;
        }

        public string Username { get => _username; set => _username = value; }
        public string Fname { get => _fname; set => _fname = value; }
        public string Lname { get => _lname; set => _lname = value; }
        public string Email { get => _email; set => _email = value; }
        public Guid Id { get => _id; set => _id = value; }
    }
}