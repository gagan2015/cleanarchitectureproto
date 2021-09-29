using TopupPortal.Application.Common.Interfaces;
using TopupPortal.Infrastructure.Files;
using TopupPortal.Infrastructure.Identity;
using TopupPortal.Infrastructure.Persistence;
using TopupPortal.Infrastructure.Persistence.Repository;
using TopupPortal.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TopupPortal.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //if (configuration.GetValue<bool>("useinmemorydatabase"))
            //{
            //    services.AddDbContext<applicationdbcontext>(options =>
            //        options.("TopupPortaldb"));
            //}
            //else
            //{
            //    services.adddbcontext<applicationdbcontext>(options =>
            //        options.usesqlserver(
            //            configuration.getconnectionstring("defaultconnection"),
            //            b => b.migrationsassembly(typeof(applicationdbcontext).assembly.fullname)));
            //}

            //services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDomainEventService, DomainEventService>();

            //services
            //    .AddDefaultIdentity<ApplicationUser>()
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();
           // services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });

            return services;
        }
    }
}