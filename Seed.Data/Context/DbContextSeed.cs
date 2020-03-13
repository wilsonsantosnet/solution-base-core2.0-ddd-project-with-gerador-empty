using Common.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Seed.Data.Map;
using Seed.Domain.Entitys;

namespace Seed.Data.Context
{
    public class DbContextSeed : DbContextMultiTenantcy
    {

        public DbContextSeed(DbContextOptions<DbContextSeed> options, CurrentUser user, IConfiguration config)
            : base(options, user, config)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new SampleMap(modelBuilder.Entity<Sample>());
            new SampleTypeMap(modelBuilder.Entity<SampleType>());

        }


    }
}
