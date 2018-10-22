﻿// <auto-generated />
using System;
using MVCThing.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MVCThing.Migrations
{
    [DbContext(typeof(MVCThingContext))]
    [Migration("20181022023806_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MVCThing.Models.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("Player1Hits");

                    b.Property<int>("Player2Hits");

                    b.Property<int>("PlayerOneID");

                    b.Property<int>("PlayerTwoID");

                    b.Property<int>("Winner");

                    b.HasKey("ID");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("MVCThing.Models.Player", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GamesPlayed");

                    b.Property<int>("GamesWon");

                    b.Property<DateTime>("JoinDate");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Player");
                });
#pragma warning restore 612, 618
        }
    }
}
