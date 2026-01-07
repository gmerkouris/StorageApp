using Microsoft.AspNetCore.Identity;

namespace SrorageApp.Models
{
    public class AppUser : IdentityUser
    {

        public string FirstName { get; set; }

        public string SurName { get; set; }

    }
}
