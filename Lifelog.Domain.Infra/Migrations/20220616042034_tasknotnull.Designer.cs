﻿// <auto-generated />
using System;
using Lifelog.Domain.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lifelog.Domain.Infra.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220616042034_tasknotnull")]
    partial class tasknotnull
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Lifelog.Domain.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("BIT")
                        .HasColumnName("Active");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("VARCHAR(16)")
                        .HasColumnName("Color");

                    b.Property<bool>("HasTimeProgress")
                        .HasColumnType("BIT")
                        .HasColumnName("HasTimeProgress");

                    b.Property<decimal>("HoursToConclude")
                        .HasMaxLength(60)
                        .HasColumnType("DECIMAL(38,17)")
                        .HasColumnName("HoursToConclude");

                    b.Property<decimal>("HoursTracked")
                        .HasMaxLength(60)
                        .HasColumnType("DECIMAL(38,17)")
                        .HasColumnName("HoursTracked");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("VARCHAR(160)")
                        .HasColumnName("Slug");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("NVARCHAR(160)")
                        .HasColumnName("Title");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("VARCHAR(160)")
                        .HasColumnName("User");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Slug" }, "IX_Project_Slug")
                        .IsUnique();

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("Lifelog.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Lifelog.Domain.Entities.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateEnd")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(60)
                        .HasColumnType("SMALLDATETIME")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("DateInit")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(60)
                        .HasColumnType("SMALLDATETIME")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Desc")
                        .HasMaxLength(1600)
                        .HasColumnType("NVARCHAR(1600)");

                    b.Property<bool>("Done")
                        .HasColumnType("BIT");

                    b.Property<int?>("ProjectId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("NVARCHAR(160)");

                    b.Property<int?>("UserId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("UserSlug")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("VARCHAR(160)");

                    b.Property<bool>("isPublic")
                        .HasColumnType("BIT");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Task", (string)null);
                });

            modelBuilder.Entity("Lifelog.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Auth2Fa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIT")
                        .HasDefaultValue(false)
                        .HasColumnName("Auth2Fa");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("VARCHAR(160)")
                        .HasColumnName("Email");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("Image");

                    b.Property<DateTime>("Joined")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(60)
                        .HasColumnType("SMALLDATETIME")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<short>("Language")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("SMALLINT")
                        .HasDefaultValue((short)1)
                        .HasColumnName("Language");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR(80)")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("VARCHAR(80)")
                        .HasColumnName("Password");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("VARCHAR(160)")
                        .HasColumnName("Slug");

                    b.Property<short>("Timezone")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("SMALLINT")
                        .HasDefaultValue((short)1)
                        .HasColumnName("Timezone");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex(new[] { "Slug" }, "IX_User_Slug")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("ProjectUser", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ProjectId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectUser");
                });

            modelBuilder.Entity("Lifelog.Domain.Entities.TaskItem", b =>
                {
                    b.HasOne("Lifelog.Domain.Entities.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Task_Project");

                    b.HasOne("Lifelog.Domain.Entities.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Task_User");

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lifelog.Domain.Entities.User", b =>
                {
                    b.HasOne("Lifelog.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_Role");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ProjectUser", b =>
                {
                    b.HasOne("Lifelog.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ProjectUser_ProjectId");

                    b.HasOne("Lifelog.Domain.Entities.Project", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ProjectUser_UserId");
                });

            modelBuilder.Entity("Lifelog.Domain.Entities.Project", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Lifelog.Domain.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Lifelog.Domain.Entities.User", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}