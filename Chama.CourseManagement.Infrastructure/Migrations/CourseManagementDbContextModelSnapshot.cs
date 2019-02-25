﻿// <auto-generated />
using System;
using Chama.CourseManagement.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Chama.CourseManagement.Infrastructure.Migrations
{
    [DbContext(typeof(CourseManagementDbContext))]
    partial class CourseManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Chama.CourseManagement.Domain.Entities.Course", b =>
                {
                    b.Property<Guid>("CourseId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CourseName");

                    b.Property<Guid?>("TeacherUserId");

                    b.Property<int>("TotalCapacity");

                    b.HasKey("CourseId");

                    b.HasIndex("TeacherUserId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Chama.CourseManagement.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("Name");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Chama.CourseManagement.Domain.Entities.UserCourse", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("CourseId");

                    b.HasKey("UserId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("UserCourse");
                });

            modelBuilder.Entity("Chama.CourseManagement.Domain.Entities.Course", b =>
                {
                    b.HasOne("Chama.CourseManagement.Domain.Entities.User", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherUserId");
                });

            modelBuilder.Entity("Chama.CourseManagement.Domain.Entities.UserCourse", b =>
                {
                    b.HasOne("Chama.CourseManagement.Domain.Entities.Course", "Course")
                        .WithMany("UserCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Chama.CourseManagement.Domain.Entities.User", "User")
                        .WithMany("UserCourses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
