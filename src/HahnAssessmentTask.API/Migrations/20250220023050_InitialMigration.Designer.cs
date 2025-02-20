﻿// <auto-generated />
using System;
using HahnAssessmentTask.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HahnAssessmentTask.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250220023050_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HahnAssessmentTask.Core.Entities.Stock", b =>
                {
                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("StockExchange")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Symbol");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("HahnAssessmentTask.Core.Entities.StockDailyInformation", b =>
                {
                    b.Property<string>("StockSymbol")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Close")
                        .HasColumnType("float");

                    b.Property<double>("High")
                        .HasColumnType("float");

                    b.Property<double>("Low")
                        .HasColumnType("float");

                    b.Property<double>("Open")
                        .HasColumnType("float");

                    b.Property<double>("Volume")
                        .HasColumnType("float");

                    b.HasKey("StockSymbol", "Date");

                    b.ToTable("StockDailyInformation");
                });

            modelBuilder.Entity("HahnAssessmentTask.Core.Entities.StockDailyInformation", b =>
                {
                    b.HasOne("HahnAssessmentTask.Core.Entities.Stock", "Stock")
                        .WithMany("StockDailyInformations")
                        .HasForeignKey("StockSymbol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("HahnAssessmentTask.Core.Entities.Stock", b =>
                {
                    b.Navigation("StockDailyInformations");
                });
#pragma warning restore 612, 618
        }
    }
}
