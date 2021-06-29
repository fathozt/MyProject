using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenimProjem.BL.Business;
using BenimProjem.BL.Business.Abstracts;
using BenimProjem.DAL;
using BenimProjem.Entities.Authentications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BenimProjem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextExtension();
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<BenimProjemDbContext>()
              .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/User/GirisYap");
                options.Cookie = new CookieBuilder
                {
                    Name = "ETicaretCookie",
                    HttpOnly = false,
                    SameSite = SameSiteMode.Lax,
                    SecurePolicy = CookieSecurePolicy.Always
                };
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
            });
            services.AddTransient<IKategoriBusiness, KategoriBusiness>();
            services.AddTransient<IUrunBusiness, UrunBusiness>();
            services.AddTransient<IMarkaBusiness, MarkaBusiness>();
            services.AddTransient<IResimBusiness, ResimBusiness>();

            services.AddTransient<ISignInManagerBusiness, SignInManagerBusiness>(x => new SignInManagerBusiness(
                 services.BuildServiceProvider().GetService<SignInManager<AppUser>>(),
                 services.BuildServiceProvider().GetService<UserManager<AppUser>>()));


            services.AddTransient<IUserManagerBusiness, UserManagerBusiness>(x => new UserManagerBusiness(
                services.BuildServiceProvider().GetService<UserManager<AppUser>>(),
                services.BuildServiceProvider().GetService<RoleManager<AppRole>>()));

            services.AddTransient<IRoleManagerBusiness, RoleManagerBusiness>(x => new RoleManagerBusiness(services.BuildServiceProvider().GetService<RoleManager<AppRole>>()));

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute("yonetimPaneliRoute", "yonetimPaneli", "yonetim-paneli/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
