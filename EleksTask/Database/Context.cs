using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EleksTask
{
    public class ApllicationContext : IdentityDbContext<ApllicationUser>
    {
        public ApllicationContext(DbContextOptions<ApllicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

    public class ApllicationUser : IdentityUser
    {
        public string FireName { get; set; }

        public string LastName { get; set; }
    }
}
