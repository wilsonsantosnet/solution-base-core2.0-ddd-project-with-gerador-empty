using Microsoft.EntityFrameworkCore;
using Score.Platform.Account.Data.Map;
using Score.Platform.Account.Domain.Entitys;

namespace Score.Platform.Account.Data.Context
{
    public class DbContextScore : DbContext
    {

        public DbContextScore(DbContextOptions<DbContextScore> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ProgramMap(modelBuilder.Entity<Program>());
            new TenantMap(modelBuilder.Entity<Tenant>());
            new ThemaMap(modelBuilder.Entity<Thema>());

        }


    }
}
