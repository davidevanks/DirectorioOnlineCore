using DataAccess.Models;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using NLog;
using Utiles;
using NLog.Extensions.Logging;
using System.Web.Mvc;

namespace AppDirectorioWeb
{
    public class Startup
    {
        #region Public Constructors

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion Public Constructors

        #region Public Properties

        public IConfiguration Configuration { get; }

        #endregion Public Properties

        #region Public Methods

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Home/Error");
                app.UseStatusCodePagesWithRedirects("/Home/Home/Error?statusCode={0}");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
           
            app.Use(async (context, next) => {
                int countValuePath = context.Request.Path.Value.Split('/').Length;
                string[] vRoute = context.Request.Path.Value.Split('/');

                var urlPersonal = vRoute[1];

                if (countValuePath==2 && (urlPersonal!=""  && urlPersonal != null))
                {
                    context.Request.Path = $"/Index/Index/{urlPersonal}";
                }
               

                await next();
            });


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Home}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
               name: "StaticPage-Default",
                pattern: "{controller=Index}/{action=Index}/{personalUrl}");
             

                endpoints.MapRazorPages();
            });

 

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DirectorioOnlineCoreContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DirectorioOnlineCoreContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<MyErrorDescriber>();

            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddRazorPages();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.Configure<MailJetSettings>(Configuration.GetSection("EmailJetKey"));

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
        }

        #endregion Public Methods
    }

    public class MyErrorDescriber: IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "Las contraseñas deben tener al menos un carácter no alfanumérico.(Como *)" }; }
        public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "Las contraseñas deben tener al menos un dígito ('0'-'9')." }; }
        public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "Las contraseñas deben tener al menos una minúscula ('a'-'z')." }; }
        public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "Las contraseñas deben tener al menos una mayúscula ('A'-'Z')." }; }
    }
}