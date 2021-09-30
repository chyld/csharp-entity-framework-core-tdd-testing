﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using lib;

namespace lib.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20210930222610_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("TagTodo", b =>
                {
                    b.Property<string>("TagsName")
                        .HasColumnType("TEXT");

                    b.Property<int>("TodosId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TagsName", "TodosId");

                    b.HasIndex("TodosId");

                    b.ToTable("TagTodo");
                });

            modelBuilder.Entity("lib.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TodoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TodoId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("lib.Tag", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Color")
                        .HasColumnType("INTEGER");

                    b.HasKey("Name");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("lib.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Due")
                        .HasColumnType("TEXT");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("TagTodo", b =>
                {
                    b.HasOne("lib.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lib.Todo", null)
                        .WithMany()
                        .HasForeignKey("TodosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("lib.Comment", b =>
                {
                    b.HasOne("lib.Todo", "Todo")
                        .WithMany("Comments")
                        .HasForeignKey("TodoId");

                    b.Navigation("Todo");
                });

            modelBuilder.Entity("lib.Todo", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}