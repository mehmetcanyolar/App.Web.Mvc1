using Microsoft.AspNetCore.Identity;

namespace App.Web.Mvc1.Identity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? City { get; set; }
        //public virtual ICollection<Post>? Posts { get; set; } // User ile Post arasında 1 e çok ilişki kurduk
        //public virtual ICollection<Setting>? Settings { get; set; }
    }
}
