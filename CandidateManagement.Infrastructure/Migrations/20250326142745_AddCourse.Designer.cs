﻿// <auto-generated />
using System;
using CandidateManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CandidateManagement.Infrastructure.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20250326142745_AddCourse")]
    partial class AddCourse
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CandidateManagement.Infrastructure.Entity.Candidate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CentreID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Forename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SwqrNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelephoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CentreID");

                    b.ToTable("Candidates");

                    b.HasData(
                        new
                        {
                            Id = new Guid("32470c7d-933f-48e3-bc75-6dee2daf40fe"),
                            CentreID = new Guid("9ef4508a-9776-4658-a459-bc97849884a7"),
                            DateOfBirth = new DateTime(1990, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "john.smith@example.com",
                            Forename = "John",
                            Surname = "Smith",
                            SwqrNumber = "10012345",
                            TelephoneNumber = "0987654321"
                        });
                });

            modelBuilder.Entity("CandidateManagement.Infrastructure.Entity.Centre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Certified")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelephoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Centres");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9ef4508a-9776-4658-a459-bc97849884a7"),
                            Address = "45 Tech Avenue, Manchester, UK",
                            Certified = true,
                            Email = "contact@techskills.com",
                            Name = "Tech Skills Academy",
                            TelephoneNumber = "0161-9876-5432"
                        });
                });

            modelBuilder.Entity("CandidateManagement.Infrastructure.Entity.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7b71bb7d-2ced-4881-b0a7-2c8e683df4d8"),
                            Name = "Computer Science"
                        });
                });

            modelBuilder.Entity("CentreCourse", b =>
                {
                    b.Property<Guid>("CentresId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CoursesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CentresId", "CoursesId");

                    b.HasIndex("CoursesId");

                    b.ToTable("CentreCourse");
                });

            modelBuilder.Entity("CandidateManagement.Infrastructure.Entity.Candidate", b =>
                {
                    b.HasOne("CandidateManagement.Infrastructure.Entity.Centre", "Centre")
                        .WithMany("Candidates")
                        .HasForeignKey("CentreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Centre");
                });

            modelBuilder.Entity("CentreCourse", b =>
                {
                    b.HasOne("CandidateManagement.Infrastructure.Entity.Centre", null)
                        .WithMany()
                        .HasForeignKey("CentresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CandidateManagement.Infrastructure.Entity.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CandidateManagement.Infrastructure.Entity.Centre", b =>
                {
                    b.Navigation("Candidates");
                });
#pragma warning restore 612, 618
        }
    }
}
