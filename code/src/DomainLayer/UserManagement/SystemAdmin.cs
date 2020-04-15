namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class SystemAdmin : Subscribed
    {
        public SystemAdmin(string uname, string pswd, string fname, string lname, string email) : base(uname, pswd, fname, lname, email)
        {
        }

        //override
        public bool isSystemAdmin()
        {
            return true;
        }
    }
}