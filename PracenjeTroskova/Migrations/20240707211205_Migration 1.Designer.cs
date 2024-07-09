﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PracenjeTroskova.Models;

#nullable disable

namespace PracenjeTroskova.Migrations
{
    [DbContext(typeof(AplikacijaBP))]
    [Migration("20240707211205_Migration 1")]
    partial class Migration1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PracenjeTroskova.Models.Kategorija", b =>
                {
                    b.Property<int>("KategorijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KategorijaID"));

                    b.Property<string>("Ikonica")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Tip")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("KategorijaID");

                    b.ToTable("Kategorije");
                });

            modelBuilder.Entity("PracenjeTroskova.Models.Transakcija", b =>
                {
                    b.Property<int>("TransakcijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransakcijaID"));

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("Iznos")
                        .HasColumnType("int");

                    b.Property<int>("KategorijaID")
                        .HasColumnType("int");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("TransakcijaID");

                    b.HasIndex("KategorijaID");

                    b.ToTable("Transakcije");
                });

            modelBuilder.Entity("PracenjeTroskova.Models.Transakcija", b =>
                {
                    b.HasOne("PracenjeTroskova.Models.Kategorija", "Kategorija")
                        .WithMany()
                        .HasForeignKey("KategorijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategorija");
                });
#pragma warning restore 612, 618
        }
    }
}