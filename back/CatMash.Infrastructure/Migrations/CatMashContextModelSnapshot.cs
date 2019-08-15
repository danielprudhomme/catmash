﻿// <auto-generated />
using System;
using CatMash.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CatMash.Infrastructure.Migrations
{
    [DbContext(typeof(CatMashContext))]
    partial class CatMashContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CatMash.Core.Entities.Cat", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Rating");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Cat");
                });

            modelBuilder.Entity("CatMash.Core.Entities.Vote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Occurence");

                    b.HasKey("Id");

                    b.ToTable("Vote");
                });

            modelBuilder.Entity("CatMash.Core.Entities.VoteCat", b =>
                {
                    b.Property<string>("CatId");

                    b.Property<Guid>("VoteId");

                    b.Property<int>("Order");

                    b.HasKey("CatId", "VoteId");

                    b.HasIndex("VoteId");

                    b.ToTable("VoteCat");
                });

            modelBuilder.Entity("CatMash.Core.Entities.VoteCat", b =>
                {
                    b.HasOne("CatMash.Core.Entities.Cat", "Cat")
                        .WithMany("VoteCats")
                        .HasForeignKey("CatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CatMash.Core.Entities.Vote", "Vote")
                        .WithMany("VoteCats")
                        .HasForeignKey("VoteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
