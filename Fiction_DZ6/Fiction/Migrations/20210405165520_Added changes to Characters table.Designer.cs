﻿// <auto-generated />
using Fiction_DZ6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fiction_DZ6.Migrations
{
    [DbContext(typeof(FictionDbContext))]
    [Migration("20210405165520_Added changes to Characters table")]
    partial class AddedchangestoCharacterstable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fiction_DZ6.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StoryId");

                    b.ToTable("Characters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 14,
                            Gender = 0,
                            Name = "Finn Mertens",
                            StoryId = 1
                        },
                        new
                        {
                            Id = 2,
                            Age = 25,
                            Gender = 0,
                            Name = "Philip Fry",
                            StoryId = 2
                        },
                        new
                        {
                            Id = 3,
                            Age = 2700,
                            Gender = 0,
                            Name = "Arven Undomiel",
                            StoryId = 3
                        });
                });

            modelBuilder.Entity("Fiction_DZ6.Models.Story", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Story");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Adventure Time"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Futurama"
                        },
                        new
                        {
                            Id = 3,
                            Name = "LOTR"
                        });
                });

            modelBuilder.Entity("Fiction_DZ6.Models.Character", b =>
                {
                    b.HasOne("Fiction_DZ6.Models.Story", "Story")
                        .WithMany("Characters")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Story");
                });

            modelBuilder.Entity("Fiction_DZ6.Models.Story", b =>
                {
                    b.Navigation("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}