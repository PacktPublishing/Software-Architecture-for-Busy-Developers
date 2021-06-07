using Microsoft.EntityFrameworkCore;
using System;

namespace patterns_mediator_api
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options):base(options)
        {
            
        }
        public DbSet<DemoEntity> DemoEntities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DemoEntity>().HasData(
                new DemoEntity { id= Guid.NewGuid(), property1 = "a first entity attr. value", property2 = "another value" },
                new DemoEntity { id= Guid.NewGuid(), property1 = "a second entity attr. value", property2 = "another value" }
            );
        }

    }
}