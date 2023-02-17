﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Moryx.Resources.Model;

#nullable disable

namespace Moryx.Resources.Management.Model.Migrations.Sqlite
{
    [DbContext(typeof(SqliteResourcesContext))]
    [Migration("20230215071938_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("Moryx.Resources.Model.ResourceEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExtensionData")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Resources", "public");
                });

            modelBuilder.Entity("Moryx.Resources.Model.ResourceRelationEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("RelationType")
                        .HasColumnType("INTEGER");

                    b.Property<long>("SourceId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SourceName")
                        .HasColumnType("TEXT");

                    b.Property<long>("TargetId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TargetName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.HasIndex("TargetId");

                    b.ToTable("ResourceRelations", "public");
                });

            modelBuilder.Entity("Moryx.Resources.Model.ResourceRelationEntity", b =>
                {
                    b.HasOne("Moryx.Resources.Model.ResourceEntity", "Source")
                        .WithMany("Targets")
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Moryx.Resources.Model.ResourceEntity", "Target")
                        .WithMany("Sources")
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Source");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("Moryx.Resources.Model.ResourceEntity", b =>
                {
                    b.Navigation("Sources");

                    b.Navigation("Targets");
                });
#pragma warning restore 612, 618
        }
    }
}
