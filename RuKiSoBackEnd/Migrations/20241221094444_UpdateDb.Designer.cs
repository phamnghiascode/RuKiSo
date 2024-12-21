﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RuKiSoBackEnd.Data;

#nullable disable

namespace RuKiSoBackEnd.Migrations
{
    [DbContext(typeof(RuKiSoDataContext))]
    [Migration("20241221094444_UpdateDb")]
    partial class UpdateDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RuKiSoBackEnd.Models.Domains.BatchIngredient", b =>
                {
                    b.Property<int>("BatchId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("BatchId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("BatchIngredients");

                    b.HasData(
                        new
                        {
                            BatchId = 1,
                            IngredientId = 3,
                            Quantity = 50
                        },
                        new
                        {
                            BatchId = 1,
                            IngredientId = 5,
                            Quantity = 1
                        },
                        new
                        {
                            BatchId = 2,
                            IngredientId = 2,
                            Quantity = 45
                        },
                        new
                        {
                            BatchId = 2,
                            IngredientId = 6,
                            Quantity = 1
                        },
                        new
                        {
                            BatchId = 3,
                            IngredientId = 3,
                            Quantity = 40
                        },
                        new
                        {
                            BatchId = 3,
                            IngredientId = 7,
                            Quantity = 1
                        },
                        new
                        {
                            BatchId = 4,
                            IngredientId = 2,
                            Quantity = 30
                        },
                        new
                        {
                            BatchId = 4,
                            IngredientId = 5,
                            Quantity = 1
                        },
                        new
                        {
                            BatchId = 5,
                            IngredientId = 1,
                            Quantity = 25
                        },
                        new
                        {
                            BatchId = 5,
                            IngredientId = 7,
                            Quantity = 1
                        },
                        new
                        {
                            BatchId = 6,
                            IngredientId = 3,
                            Quantity = 20
                        },
                        new
                        {
                            BatchId = 6,
                            IngredientId = 6,
                            Quantity = 1
                        },
                        new
                        {
                            BatchId = 7,
                            IngredientId = 4,
                            Quantity = 15
                        },
                        new
                        {
                            BatchId = 7,
                            IngredientId = 5,
                            Quantity = 1
                        },
                        new
                        {
                            BatchId = 8,
                            IngredientId = 3,
                            Quantity = 20
                        },
                        new
                        {
                            BatchId = 8,
                            IngredientId = 8,
                            Quantity = 2
                        },
                        new
                        {
                            BatchId = 9,
                            IngredientId = 2,
                            Quantity = 50
                        },
                        new
                        {
                            BatchId = 9,
                            IngredientId = 6,
                            Quantity = 1
                        },
                        new
                        {
                            BatchId = 10,
                            IngredientId = 1,
                            Quantity = 45
                        },
                        new
                        {
                            BatchId = 10,
                            IngredientId = 5,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("RuKiSoBackEnd.Models.Domains.Batches", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EstimateEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Yield")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Batches");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EstimateEndDate = new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 1,
                            StartDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Yield = 0
                        },
                        new
                        {
                            Id = 2,
                            EstimateEndDate = new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 2,
                            StartDate = new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Yield = 0
                        },
                        new
                        {
                            Id = 3,
                            EstimateEndDate = new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 3,
                            StartDate = new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Yield = 0
                        },
                        new
                        {
                            Id = 4,
                            EstimateEndDate = new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 4,
                            StartDate = new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Yield = 0
                        },
                        new
                        {
                            Id = 5,
                            EstimateEndDate = new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 5,
                            StartDate = new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Yield = 0
                        },
                        new
                        {
                            Id = 6,
                            EstimateEndDate = new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 6,
                            StartDate = new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Yield = 0
                        },
                        new
                        {
                            Id = 7,
                            EstimateEndDate = new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 7,
                            StartDate = new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Yield = 0
                        },
                        new
                        {
                            Id = 8,
                            EstimateEndDate = new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 8,
                            StartDate = new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Yield = 0
                        },
                        new
                        {
                            Id = 9,
                            EstimateEndDate = new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 1,
                            StartDate = new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Yield = 0
                        },
                        new
                        {
                            Id = 10,
                            EstimateEndDate = new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductId = 2,
                            StartDate = new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Yield = 0
                        });
                });

            modelBuilder.Entity("RuKiSoBackEnd.Models.Domains.Ingredients", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PurchasePrice")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Gạo nếp",
                            PurchasePrice = 20000.0,
                            Quantity = 300,
                            Unit = "Kg"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Nếp cái hoa vàng",
                            PurchasePrice = 25000.0,
                            Quantity = 500,
                            Unit = "Kg"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Nếp đen",
                            PurchasePrice = 23000.0,
                            Quantity = 1000,
                            Unit = "Kg"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Đòng đòng",
                            PurchasePrice = 30000.0,
                            Quantity = 50,
                            Unit = "Kg"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Men thuốc bắc",
                            PurchasePrice = 150000.0,
                            Quantity = 3,
                            Unit = "Kg"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Men thường",
                            PurchasePrice = 80000.0,
                            Quantity = 1,
                            Unit = "Kg"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Men lá",
                            PurchasePrice = 100000.0,
                            Quantity = 2,
                            Unit = "Kg"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Táo mèo",
                            PurchasePrice = 30000.0,
                            Quantity = 10,
                            Unit = "Kg"
                        });
                });

            modelBuilder.Entity("RuKiSoBackEnd.Models.Domains.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Nếp đen, men thuốc bắc",
                            Name = "Rượu trắng 45",
                            Price = 50000.0,
                            Quantity = 500
                        },
                        new
                        {
                            Id = 2,
                            Description = "Nếp đen, men thuốc bắc",
                            Name = "Rượu trắng 40",
                            Price = 45000.0,
                            Quantity = 300
                        },
                        new
                        {
                            Id = 3,
                            Description = "Nếp đen, men thuốc bắc",
                            Name = "Rượu trắng 35",
                            Price = 40000.0,
                            Quantity = 250
                        },
                        new
                        {
                            Id = 4,
                            Description = "Nếp cái hoa vàng, men thuốc bắc",
                            Name = "Đòng đòng 45",
                            Price = 75000.0,
                            Quantity = 80
                        },
                        new
                        {
                            Id = 5,
                            Description = "Nếp cái hoa vàng, men thuốc bắc",
                            Name = "Đòng đòng 40",
                            Price = 70000.0,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 6,
                            Description = "Nếp cái hoa vàng, men thuốc bắc",
                            Name = "Đòng đòng 35",
                            Price = 60000.0,
                            Quantity = 50
                        },
                        new
                        {
                            Id = 7,
                            Description = "Gạo nếp, men lá",
                            Name = "Rượu bách nhật",
                            Price = 40000.0,
                            Quantity = 10
                        },
                        new
                        {
                            Id = 8,
                            Description = "Nếp đen, táo mèo",
                            Name = "Rượu táo mèo",
                            Price = 60000.0,
                            Quantity = 20
                        });
                });

            modelBuilder.Entity("RuKiSoBackEnd.Models.Domains.Transactions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("IngredientId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("TranDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("TranType")
                        .HasColumnType("bit");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("ProductId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("RuKiSoBackEnd.Models.Domains.BatchIngredient", b =>
                {
                    b.HasOne("RuKiSoBackEnd.Models.Domains.Batches", "Batch")
                        .WithMany("BatchIngredients")
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RuKiSoBackEnd.Models.Domains.Ingredients", "Ingredient")
                        .WithMany("BatchIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Batch");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("RuKiSoBackEnd.Models.Domains.Batches", b =>
                {
                    b.HasOne("RuKiSoBackEnd.Models.Domains.Products", "Product")
                        .WithMany("Batches")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Product");
                });

            modelBuilder.Entity("RuKiSoBackEnd.Models.Domains.Transactions", b =>
                {
                    b.HasOne("RuKiSoBackEnd.Models.Domains.Ingredients", "Ingredient")
                        .WithMany("Transactions")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("RuKiSoBackEnd.Models.Domains.Products", "Product")
                        .WithMany("Transactions")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Ingredient");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("RuKiSoBackEnd.Models.Domains.Batches", b =>
                {
                    b.Navigation("BatchIngredients");
                });

            modelBuilder.Entity("RuKiSoBackEnd.Models.Domains.Ingredients", b =>
                {
                    b.Navigation("BatchIngredients");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("RuKiSoBackEnd.Models.Domains.Products", b =>
                {
                    b.Navigation("Batches");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
