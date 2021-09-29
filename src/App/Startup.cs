using App.Filters;
using App.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Linq;
using TopupPortal.Application;
using TopupPortal.Application.Common.Interfaces;
using TopupPortal.Infrastructure;
using TopupPortal.Infrastructure.Identity;
using TopupPortal.Infrastructure.Persistence.Repository;

namespace App
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
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            //services.AddHealthChecks()
            //    .AddDbContextCheck<ApplicationDbContext>();

            services.AddControllersWithViews(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
                    .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

            services.AddRazorPages();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

      

            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "TopupPortal API";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            // services.AddDbContext<DapperContext>();
            string dbconection = Configuration.GetConnectionString("DefaultConnection");


            // services.AddTransient<IDbConnectionFactory>((sp) => new SqlConnection(dbConnectionString));
            services.AddTransient<IDbConnectionFactory, SqlServerDbConnectionFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddIdentity<TopupPortal.Application.Common.Models.Identity.ApplicationUser, TopupPortal.Application.Common.Models.Identity.ApplicationUserRole>()
                     .AddUserStore<CustomUserStore>()
                     .AddRoleStore<CustomRoleStore>()
                     .AddDefaultTokenProviders();
            //
            //services.AddIdentity<AspNetCore.Identity.Dapper.Models.ApplicationUser, ApplicationRole>()

            //    .AddDefaultTokenProviders();
            //services.AddTransient<IUserStore<AspNetCore.Identity.Dapper.Models.ApplicationUser>, CustomUserStore>();
            // services.AddTransient<IRoleStore<ApplicationUserRole>, CustomRoleStore>();
            //services.AddIdentityCore<AspNetCore.Identity.Dapper.Models.ApplicationUser>(options =>
            //{
            //    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //    options.User.RequireUniqueEmail = true;

            //    options.Password.RequireDigit = true;
            //    options.Password.RequiredLength = 8;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = true;
            //})
            //.AddRoles<ApplicationRole>()
            //.AddDapperStores(options =>
            // {
            //     options.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            //     options.DbSchema = "payroll schema";
            // });



        }

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
          

            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            app.UseRouting();

            app.UseAuthentication();
            //app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

          
        }
    }
}
