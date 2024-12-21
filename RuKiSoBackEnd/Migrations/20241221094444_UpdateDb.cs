using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RuKiSoBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "BatchIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                    { 2, "Nếp đen, men thuốc bắc", "Rượu trắng 40", 45000.0, 300 },
                    { 3, "Nếp đen, men thuốc bắc", "Rượu trắng 35", 40000.0, 250 },
                    { 4, "Nếp cái hoa vàng, men thuốc bắc", "Đòng đòng 45", 75000.0, 80 },
                    { 5, "Nếp cái hoa vàng, men thuốc bắc", "Đòng đòng 40", 70000.0, 100 },
                    { 6, "Nếp cái hoa vàng, men thuốc bắc", "Đòng đòng 35", 60000.0, 50 },
                    { 7, "Gạo nếp, men lá", "Rượu bách nhật", 40000.0, 10 },
                    { 8, "Nếp đen, táo mèo", "Rượu táo mèo", 60000.0, 20 }
                });

            migrationBuilder.InsertData(
                table: "Batches",
                columns: new[] { "Id", "EstimateEndDate", "ProductId", "StartDate", "Yield" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 2, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 3, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 4, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 5, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 6, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 7, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 8, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 9, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 10, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 }
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
                    { 3, 3, 40 },
                    { 3, 7, 1 },
                    { 4, 2, 30 },
                    { 4, 5, 1 },
                    { 5, 1, 25 },
                    { 5, 7, 1 },
                    { 6, 3, 20 },
                    { 6, 6, 1 },
                    { 7, 4, 15 },
                    { 7, 5, 1 },
                    { 8, 3, 20 },
                    { 8, 8, 2 },
                    { 9, 2, 50 },
                    { 9, 6, 1 },
                    { 10, 1, 45 },
                    { 10, 5, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 7, 5 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 8, 3 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 8, 8 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 9, 6 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "BatchIngredients",
                keyColumns: new[] { "BatchId", "IngredientId" },
                keyValues: new object[] { 10, 5 });

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Batches",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BatchIngredients");
        }
    }
}
