using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Stopify.Common;
using Stopify.Models;
using System.Threading.Tasks;

namespace Stopify.Data
{
    public static class SeedUserRoles
    {
        public static IApplicationBuilder UseDatabaseSeedWithIdentities(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<StopifyDbContext>();

                context.Database.EnsureCreated();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task.Run(async () =>
                {
                    var roles = new[]
                    {
                            RoleConstants.ADMIN_ROLE,
                            RoleConstants.USER_ROLE,
                        };

                    foreach (var role in roles)
                    {
                        bool roleExists = await roleManager.RoleExistsAsync(role);

                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole { Name = role });
                        }
                    }

                    var adminRole = RoleConstants.ADMIN_ROLE;
                    var adminEmail = "admin@gmail.com";

                    var adminUser = new User
                    {                       
                        Email = adminEmail,
                        UserName = adminRole,
                    };

                    await userManager.CreateAsync(adminUser, "admin123");

                    await userManager.AddToRoleAsync(adminUser, adminRole);

                })
                .Wait();
            }

            return app;
        }
    }
}
