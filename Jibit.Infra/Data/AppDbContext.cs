using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jibit.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Jibit.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Request> Request { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.ServiceName).IsRequired().HasMaxLength(100);
                entity.Property(r => r.Provider).IsRequired().HasMaxLength(100);
                entity.Property(r => r.CalledAt).IsRequired();
                entity.Property(r => r.IsSuccessful).IsRequired();
                entity.Property(r => r.ErrorMessage).HasMaxLength(500);
            });
        }
    }
}