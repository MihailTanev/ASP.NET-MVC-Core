namespace Eventures.Web.Middleware
{
    using Eventures.Common;
    using Eventures.Data;
    using Eventures.Domain;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    public static class SeedIdentitiesMiddleware
    {
        public static IApplicationBuilder UseDatabaseSeedWithIdentities(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<EventuresDbContext>();

                context.Database.EnsureCreated();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<EventuresUser>>();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task.Run(async () =>
                    {
                        var roles = new[]
                        {
                            GlobalConstants.ADMIN_ROLE,
                            GlobalConstants.USER_ROLE,
                        };

                       foreach (var role in roles)
                        {
                            bool roleExists = await roleManager.RoleExistsAsync(role);

                            if (!roleExists)
                            {
                                await roleManager.CreateAsync(new IdentityRole { Name = role });
                            }
                        }

                        var adminRole = GlobalConstants.ADMIN_ROLE;
                        var adminEmail = "admin@gmail.com";
                        var ucn = "56412566";

                        var adminUser = new EventuresUser
                        {
                            FirstName = adminRole,
                            LastName = adminRole,
                            Email = adminEmail,
                            UserName = adminRole,
                            Ucn = ucn,
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
