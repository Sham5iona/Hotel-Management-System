using Microsoft.AspNetCore.Identity;

namespace HMS.Areas.Identity.Model
{
    public class Admin : IdentityUser
    {
        private Guid _admin_id;
        public Guid AdminId { get { return _admin_id; }
                             set { _admin_id = value; } }

        private bool _is_admin;
        public bool IsAdmin { get { return _is_admin; } 
                              set { _is_admin = value; } }
        public Admin()
        {
            
        }

        public Admin(string username, string password, bool is_admin)
        {
            this.UserName = username;
            this.PasswordHash = password;
            this.IsAdmin = is_admin;
        }
    }
}
