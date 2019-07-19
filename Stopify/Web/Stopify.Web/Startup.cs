using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stopify.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stopify.Models;
using Stopify.Services;
using CloudinaryDotNet;
using Stopify.Services.Mapping;
using Stopify.Web.InputModels;
using System.Reflection;
using Stopify.Web.ViewModels.Home.Index;
using Stopify.Services.Models;

namespace Stopify.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Configure Db context
            services.AddDbContext<StopifyDbContext>(options =>
                           options.UseSqlServer(
                               Configuration.GetConnectionString("DefaultConnection")));

            // Configure Identity
            services.AddIdentity<User, IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<StopifyDbContext>()
                .AddDefaultTokenProviders();

            Account cloudinaryCredential = new Account(
                this.Configuration["Cloudinary:CloudName"],
                this.Configuration["Cloudinary:ApiKey"],
                this.Configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinary = new Cloudinary(cloudinaryCredential);

            services.AddSingleton(cloudinary);

            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 5;
            });

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(
                 typeof(CreateProductInputModel).GetTypeInfo().Assembly,
                 typeof(ProductHomeViewModel).GetTypeInfo().Assembly,
                 typeof(ProductServiceModel).GetTypeInfo().Assembly);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // seed roles
            app.UseDatabaseSeedWithIdentities();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                     name: "default",
                     template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
