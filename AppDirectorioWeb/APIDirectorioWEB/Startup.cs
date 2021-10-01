
using Identity.Dapper;
using Identity.Dapper.Entities;
using Identity.Dapper.Models;
using Identity.Dapper.PostgreSQL.Connections;
using Identity.Dapper.PostgreSQL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace APIDirectorioWEB
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
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIDirectorioWEB v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

          
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           

            var identityConnectionString = Configuration.GetSection("DapperIdentity");

            services.ConfigureDapperConnectionProvider<PostgreSqlConnectionProvider>(identityConnectionString)
                .ConfigureDapperIdentityCryptography(Configuration.GetSection("DapperIdentityCryptography"))
                .ConfigureDapperIdentityOptions(new DapperIdentityOptions { UseTransactionalBehavior = false });

            services.AddIdentity<DapperIdentityUser, DapperIdentityRole>(x =>
             {
                 x.Password.RequireDigit = false;
                 x.Password.RequiredLength = 1;
                 x.Password.RequireLowercase = false;
                 x.Password.RequireNonAlphanumeric = false;
                 x.Password.RequireUppercase = false;
             })
             .AddDapperIdentityFor<PostgreSqlConfiguration>()
             .AddDefaultTokenProviders();
            services.Configure<CookiePolicyOptions>(options =>
            {
             
             options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIDirectorioWEB", Version = "v1" });
            });
        }

        #endregion Public Methods
    }
}