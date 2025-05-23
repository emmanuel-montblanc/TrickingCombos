﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrickingCombos.API.Data;

#nullable disable

namespace TrickingCombos.API.Migrations
{
    [DbContext(typeof(TricksDbContext))]
    partial class TricksDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.4");

            modelBuilder.Entity("StanceTransition", b =>
                {
                    b.Property<Guid>("StancesId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TransitionsId")
                        .HasColumnType("TEXT");

                    b.HasKey("StancesId", "TransitionsId");

                    b.HasIndex("TransitionsId");

                    b.ToTable("StanceTransition");
                });

            modelBuilder.Entity("TrickVariation", b =>
                {
                    b.Property<Guid>("TricksId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("VariationsId")
                        .HasColumnType("TEXT");

                    b.HasKey("TricksId", "VariationsId");

                    b.HasIndex("VariationsId");

                    b.ToTable("TrickVariation");
                });

            modelBuilder.Entity("TrickingCombos.API.Models.Stance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Stances");
                });

            modelBuilder.Entity("TrickingCombos.API.Models.Transition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("TrickId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TrickId");

                    b.ToTable("Transitions");
                });

            modelBuilder.Entity("TrickingCombos.API.Models.Trick", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DefaultLandingStanceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DefaultLandingStanceId");

                    b.ToTable("Tricks");
                });

            modelBuilder.Entity("TrickingCombos.API.Models.Variation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("LandingStanceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("StanceId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LandingStanceId");

                    b.HasIndex("StanceId");

                    b.ToTable("Variations");
                });

            modelBuilder.Entity("StanceTransition", b =>
                {
                    b.HasOne("TrickingCombos.API.Models.Stance", null)
                        .WithMany()
                        .HasForeignKey("StancesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrickingCombos.API.Models.Transition", null)
                        .WithMany()
                        .HasForeignKey("TransitionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrickVariation", b =>
                {
                    b.HasOne("TrickingCombos.API.Models.Trick", null)
                        .WithMany()
                        .HasForeignKey("TricksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrickingCombos.API.Models.Variation", null)
                        .WithMany()
                        .HasForeignKey("VariationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrickingCombos.API.Models.Transition", b =>
                {
                    b.HasOne("TrickingCombos.API.Models.Trick", null)
                        .WithMany("Transitions")
                        .HasForeignKey("TrickId");
                });

            modelBuilder.Entity("TrickingCombos.API.Models.Trick", b =>
                {
                    b.HasOne("TrickingCombos.API.Models.Stance", "DefaultLandingStance")
                        .WithMany()
                        .HasForeignKey("DefaultLandingStanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DefaultLandingStance");
                });

            modelBuilder.Entity("TrickingCombos.API.Models.Variation", b =>
                {
                    b.HasOne("TrickingCombos.API.Models.Stance", "LandingStance")
                        .WithMany()
                        .HasForeignKey("LandingStanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrickingCombos.API.Models.Stance", null)
                        .WithMany("Variations")
                        .HasForeignKey("StanceId");

                    b.Navigation("LandingStance");
                });

            modelBuilder.Entity("TrickingCombos.API.Models.Stance", b =>
                {
                    b.Navigation("Variations");
                });

            modelBuilder.Entity("TrickingCombos.API.Models.Trick", b =>
                {
                    b.Navigation("Transitions");
                });
#pragma warning restore 612, 618
        }
    }
}
