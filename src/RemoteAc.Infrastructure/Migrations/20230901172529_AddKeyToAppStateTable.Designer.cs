﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RemoteAc.Infrastructure.Context;

#nullable disable

namespace RemoteAc.Infrastructure.Migrations
{
    [DbContext(typeof(RemoteAcContext))]
    [Migration("20230901172529_AddKeyToAppStateTable")]
    partial class AddKeyToAppStateTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("RemoteAc.Core.Entities.AppState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("AcUnitOn")
                        .HasColumnType("INTEGER");

                    b.Property<string>("WeatherStation")
                        .HasColumnType("TEXT");

                    b.Property<string>("WxGridPoints")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AppState");
                });

            modelBuilder.Entity("RemoteAc.Core.Entities.DhtSensorData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<double>("Humidity")
                        .HasColumnType("REAL");

                    b.Property<double>("TempC")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("DhtSensorData");
                });
#pragma warning restore 612, 618
        }
    }
}
