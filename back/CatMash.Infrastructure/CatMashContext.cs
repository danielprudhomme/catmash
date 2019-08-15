using CatMash.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatMash.Infrastructure
{
    public class CatMashContext : DbContext
    {
        public CatMashContext(DbContextOptions<CatMashContext> options) : base(options)
        {
        }

        public DbSet<Cat> Cats { get; set; }
        public DbSet<VoteStat> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cat>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Cat>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<VoteStat>()
                .HasKey(x => new { x.Cat1Id, x.Cat2Id });
        }
    }
}
