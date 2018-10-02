using System;
using Microsoft.Extensions.DependencyInjection;

namespace EleksTask
{
    public class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationContext>();
            context.Database.EnsureCreated();
        }
    }
}
