using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EleksTask
{
    public class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApllicationContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApllicationUser>>();
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                var user = new ApllicationUser()
                {
                    Email = "ivan.kiselichnik@gmail.com",
                    FireName = "Admin",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                userManager.CreateAsync(user, "qwerty").GetAwaiter().GetResult();
            }
        }
    }
}
