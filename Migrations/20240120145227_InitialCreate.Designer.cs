﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tech_task.Models;

#nullable disable

namespace tech_task.Migrations
{
    [DbContext(typeof(ConfigContext))]
    [Migration("20240120145227_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("tech_task.Models.ConfigurationItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("ConfigurationItems");
                });

            modelBuilder.Entity("tech_task.Models.JSONFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RootElementId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("JSONFiles");
                });

            modelBuilder.Entity("tech_task.Models.JSONLeaf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("JSONLeaves");
                });

            modelBuilder.Entity("tech_task.Models.JSONNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("JSONFileId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JSONFileId")
                        .IsUnique();

                    b.HasIndex("ParentId");

                    b.ToTable("JSONNodes");
                });

            modelBuilder.Entity("tech_task.Models.ConfigurationItem", b =>
                {
                    b.HasOne("tech_task.Models.ConfigurationItem", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("tech_task.Models.JSONLeaf", b =>
                {
                    b.HasOne("tech_task.Models.JSONNode", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("tech_task.Models.JSONNode", b =>
                {
                    b.HasOne("tech_task.Models.JSONFile", "JSONFile")
                        .WithOne("RootElement")
                        .HasForeignKey("tech_task.Models.JSONNode", "JSONFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tech_task.Models.JSONNode", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("JSONFile");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("tech_task.Models.ConfigurationItem", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("tech_task.Models.JSONFile", b =>
                {
                    b.Navigation("RootElement")
                        .IsRequired();
                });

            modelBuilder.Entity("tech_task.Models.JSONNode", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}
