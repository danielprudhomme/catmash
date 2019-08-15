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
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cat>().ToTable("Cat");
            modelBuilder.Entity<Cat>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Vote>().ToTable("Vote");
            modelBuilder.Entity<Vote>()
                .HasKey(v => v.Id);

            modelBuilder.Entity<VoteCat>()
                .HasKey(vc => new { vc.CatId, vc.VoteId });
            modelBuilder.Entity<VoteCat>()
                .HasOne(vc => vc.Cat)
                .WithMany(c => c.VoteCats)
                .HasForeignKey(vc => vc.CatId);
            modelBuilder.Entity<VoteCat>()
                .HasOne(vc => vc.Vote)
                .WithMany(v => v.VoteCats)
                .HasForeignKey(vc => vc.VoteId);
        }
    }
}
