﻿// <auto-generated />
using System;
using Group_Project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Group_Project.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201020175005_Course1")]
    partial class Course1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Group_Project.Models.Course", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AMPM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CourseNumber")
                        .HasColumnType("int");

                    b.Property<string>("CourseTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreditHours")
                        .HasColumnType("int");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Friday")
                        .HasColumnType("int");

                    b.Property<int>("InstructorID")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxStudents")
                        .HasColumnType("int");

                    b.Property<int>("Monday")
                        .HasColumnType("int");

                    b.Property<int>("Saturday")
                        .HasColumnType("int");

                    b.Property<int>("Sunday")
                        .HasColumnType("int");

                    b.Property<int>("Thursday")
                        .HasColumnType("int");

                    b.Property<int>("Tuesday")
                        .HasColumnType("int");

                    b.Property<int>("Wednesday")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("Group_Project.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Passwrd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("Zip")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
