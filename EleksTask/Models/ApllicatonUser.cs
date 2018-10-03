using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EleksTask.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }

}
