using Core.Configuration;
using login.EntiryFrameWorkCore.EntityFramWorkCore;
using login.EntiryFrameWorkCore.EntityFramWorkCore.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace login.EntiryFrameWorkCore
{
    public static class Extensions
    {
        public static void AddEntityframeWorkCore(this IServiceCollection services)
        {
            services.AddDbContext<IdentityCtmDbContext>((provider, options) =>
            {
                var Configuration = provider.GetService<IConfiguration>();
                var aa = Configuration.GetConnectionString("DefaultConnect");
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnect"));
            }, ServiceLifetime.Scoped);
            services.AddTransient<IDbConnection>(provider => provider.GetRequiredService<IdentityCtmDbContext>().Connection);
            services.AddTransient<IIdentityCtmDbContext>(provider => provider.GetRequiredService<IdentityCtmDbContext>());
        }
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<IdentityCtmDbContext>()
    .AddDefaultTokenProviders();

            //services.Configure<IdentityOptions>(option =>
            //{
            //    option.Password.RequiredLength = 8;
            //});

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryApiScopes(Config.GetApiScopes())
                .AddAspNetIdentity<User>();

            services.AddAuthentication("MyCookie")
                .AddCookie("MyCookie", option =>
                {
                    option.ExpireTimeSpan = new TimeSpan(5, 0, 0);
                });
        }
    }
}
