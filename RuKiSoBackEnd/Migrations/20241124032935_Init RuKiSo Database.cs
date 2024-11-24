using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RuKiSoBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class InitRuKiSoDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PurchasePrice = table.Column<double>(type: "float", nullable: false),
                    BatchId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Batches_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Batches",
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

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "BatchId", "Name", "PurchasePrice", "Quantity", "Unit" },
                values: new object[,]
                {
                    { 1, null, "Gạo nếp", 20000.0, 300, "Kg" },
                    { 2, null, "Nếp cái hoa vàng", 25000.0, 500, "Kg" },
                    { 3, null, "Nếp đen", 23000.0, 1000, "Kg" },
                    { 4, null, "Đòng đòng", 30000.0, 50, "Kg" },
                    { 5, null, "Men thuốc bắc", 150000.0, 3, "Kg" },
                    { 6, null, "Men thường", 80000.0, 1, "Kg" },
                    { 7, null, "Men lá", 100000.0, 2, "Kg" },
                    { 8, null, "Táo mèo", 30000.0, 10, "Kg" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Nếp đen, men thuốc bắc", "Rượu trắng 45", 50000.0, 500 },
                    { 2, "Nếp đen, men thuốc bắc", "Rượu trắng 40", 45000.0, 300 },
                    { 3, "Nếp đen, men thuốc bắc", "Rượu trắng 35", 40000.0, 250 },
                    { 4, "Nếp cái hoa vàng, men thuốc bắc", "Đòng đòng 45", 75000.0, 80 },
                    { 5, "Nếp cái hoa vàng, men thuốc bắc", "Đòng đòng 40", 70000.0, 100 },
                    { 6, "Nếp cái hoa vàng, men thuốc bắc", "Đòng đòng 35", 60000.0, 50 },
                    { 7, "Gạo nếp, men lá", "Rượu bách nhật", 40000.0, 10 },
                    { 8, "Nếp đen, táo mèo", "Rượu táo mèo", 60000.0, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Batches_ProductId",
                table: "Batches",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_BatchId",
                table: "Ingredients",
                column: "BatchId");

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
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Batches");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
