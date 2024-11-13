using Microsoft.AspNetCore.Identity;

namespace WebClient.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
