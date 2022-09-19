using login.EntiryFrameWorkCore.EntityFramWorkCore.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using Core.DbInteract;

namespace login.EntiryFrameWorkCore.EntityFramWorkCore
{
    public class IdentityCtmDbContext : IdentityDbContext<User>, IIdentityCtmDbContext, IAppDbContext
    {
        private IConfiguration _configuration;
        public IdentityCtmDbContext(DbContextOptions<IdentityCtmDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public IDbConnection Connection => Database.GetDbConnection();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityRole>().ToTable("Roles");
        }
    }
}
