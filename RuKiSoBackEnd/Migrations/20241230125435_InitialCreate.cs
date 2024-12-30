using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RuKiSoBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PurchasePrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Batches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimateEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Yield = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Batches_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TranType = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Transactions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BatchIngredients",
                columns: table => new
                {
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchIngredients", x => new { x.BatchId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_BatchIngredients_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BatchIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name", "PurchasePrice", "Quantity", "Unit" },
                values: new object[,]
                {
                    { 1, "Gạo nếp", 20000.0, 300, "Kg" },
                    { 2, "Nếp cái hoa vàng", 25000.0, 500, "Kg" },
                    { 3, "Nếp đen", 23000.0, 1000, "Kg" },
                    { 4, "Đòng đòng", 30000.0, 50, "Kg" },
                    { 5, "Men thuốc bắc", 150000.0, 3, "Kg" },
                    { 6, "Men thường", 80000.0, 1, "Kg" },
                    { 7, "Men lá", 100000.0, 2, "Kg" },
                    { 8, "Táo mèo", 30000.0, 10, "Kg" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Nếp đen, men thuốc bắc", "Rượu trắng 45", 50000.0, 500 },
                    { 2, "Nếp cái hoa vàng, men thường", "Rượu trắng 40", 45000.0, 300 },
                    { 3, "Gạo nếp, men lá", "Rượu trắng 35", 40000.0, 250 },
                    { 4, "Đòng đòng, men thuốc bắc", "Đòng đòng 45", 75000.0, 80 },
                    { 5, "Đòng đòng, men thường", "Đòng đòng 40", 70000.0, 100 },
                    { 6, "Đòng đòng, men lá", "Đòng đòng 35", 60000.0, 50 },
                    { 7, "Nếp cái hoa vàng, men lá", "Rượu bách nhật", 40000.0, 10 },
                    { 8, "Nếp đen, táo mèo, men thuốc bắc", "Rượu táo mèo", 60000.0, 20 }
                });

            migrationBuilder.InsertData(
                table: "Batches",
                columns: new[] { "Id", "EstimateEndDate", "ProductId", "StartDate", "Yield" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 2, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 3, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 4, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 5, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 6, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 7, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 8, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "IngredientId", "Name", "ProductId", "Quantity", "TranDate", "TranType", "Value" },
                values: new object[,]
                {
                    { 1, 3, "Mua nếp đen", null, 200, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 4600000.0 },
                    { 2, 5, "Mua men thuốc bắc", null, 2, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 300000.0 },
                    { 3, null, "Bán rượu trắng 45", 1, 100, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 5000000.0 },
                    { 4, null, "Bán đòng đòng 45", 4, 30, new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2250000.0 },
                    { 5, 2, "Mua nếp cái hoa vàng", null, 150, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3750000.0 },
                    { 6, 4, "Mua đòng đòng", null, 50, new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1500000.0 },
                    { 7, null, "Bán rượu trắng 40", 2, 120, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 5400000.0 },
                    { 8, null, "Bán đòng đòng 40", 5, 40, new DateTime(2024, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2800000.0 },
                    { 9, 1, "Mua gạo nếp", null, 180, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3600000.0 },
                    { 10, 7, "Mua men lá", null, 2, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 200000.0 },
                    { 11, null, "Bán rượu trắng 35", 3, 150, new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 6000000.0 },
                    { 12, null, "Bán đòng đòng 35", 6, 35, new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2100000.0 },
                    { 13, 3, "Mua nếp đen", null, 220, new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 5060000.0 },
                    { 14, 8, "Mua táo mèo", null, 10, new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 300000.0 },
                    { 15, null, "Bán rượu trắng 45", 1, 130, new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 6500000.0 },
                    { 16, null, "Bán rượu táo mèo", 8, 40, new DateTime(2024, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2400000.0 },
                    { 17, 2, "Mua nếp cái hoa vàng", null, 160, new DateTime(2024, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 4000000.0 },
                    { 18, 5, "Mua men thuốc bắc", null, 2, new DateTime(2024, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 300000.0 },
                    { 19, null, "Bán rượu trắng 40", 2, 140, new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 6300000.0 },
                    { 20, null, "Bán bách nhật", 7, 45, new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1800000.0 },
                    { 21, 4, "Mua đòng đòng", null, 100, new DateTime(2024, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3000000.0 },
                    { 22, 7, "Mua men lá", null, 3, new DateTime(2024, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 300000.0 },
                    { 23, null, "Bán đòng đòng 45", 4, 50, new DateTime(2024, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3750000.0 },
                    { 24, null, "Bán đòng đòng 40", 5, 60, new DateTime(2024, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 4200000.0 },
                    { 25, 3, "Mua nếp đen", null, 180, new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 4140000.0 },
                    { 26, 5, "Mua men thuốc bắc", null, 2, new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 300000.0 },
                    { 27, null, "Bán rượu trắng 45", 1, 110, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 5500000.0 },
                    { 28, null, "Bán rượu táo mèo", 8, 50, new DateTime(2024, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3000000.0 },
                    { 29, 2, "Mua nếp cái hoa vàng", null, 170, new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 4250000.0 },
                    { 30, 8, "Mua táo mèo", null, 15, new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 450000.0 },
                    { 31, null, "Bán rượu trắng 40", 2, 130, new DateTime(2024, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 5850000.0 },
                    { 32, null, "Bán bách nhật", 7, 40, new DateTime(2024, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1600000.0 },
                    { 33, 1, "Mua gạo nếp", null, 200, new DateTime(2024, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 4000000.0 },
                    { 34, 7, "Mua men lá", null, 2, new DateTime(2024, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 200000.0 },
                    { 35, null, "Bán rượu trắng 35", 3, 140, new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 5600000.0 },
                    { 36, null, "Bán đòng đòng 35", 6, 45, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2700000.0 },
                    { 37, 4, "Mua đòng đòng", null, 80, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2400000.0 },
                    { 38, 5, "Mua men thuốc bắc", null, 3, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 450000.0 },
                    { 39, null, "Bán đòng đòng 45", 4, 45, new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3375000.0 },
                    { 40, null, "Bán rượu trắng 45", 1, 120, new DateTime(2024, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 6000000.0 }
                });

            migrationBuilder.InsertData(
                table: "BatchIngredients",
                columns: new[] { "BatchId", "IngredientId", "Quantity" },
                values: new object[,]
                {
                    { 1, 3, 50 },
                    { 1, 5, 1 },
                    { 2, 2, 45 },
                    { 2, 6, 1 },
                    { 3, 1, 40 },
                    { 3, 7, 1 },
                    { 4, 4, 30 },
                    { 4, 5, 1 },
                    { 5, 4, 25 },
                    { 5, 6, 1 },
                    { 6, 4, 20 },
                    { 6, 7, 1 },
                    { 7, 2, 35 },
                    { 7, 7, 1 },
                    { 8, 3, 40 },
                    { 8, 5, 1 },
                    { 8, 8, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Batches_ProductId",
                table: "Batches",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchIngredients_IngredientId",
                table: "BatchIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_IngredientId",
                table: "Transactions",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ProductId",
                table: "Transactions",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatchIngredients");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
