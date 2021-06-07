using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patterns_repository
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {

        }
        public DbSet<DemoEntity> DemoEntities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DemoEntity>().HasData(
                new DemoEntity { id = 1, property1 = "a first entity attr. value", property2 = new Random().Next(0,10) },
                new DemoEntity { id = 2, property1 = "a second entity attr. value", property2 = new Random().Next(0, 10) }
            );
        }

    }
}
