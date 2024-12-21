using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RuKiSoBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class addmoredata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Batches_BatchId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_BatchId",
                table: "Ingredients");

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
                name: "BatchId",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "BatchesIngredients",
                columns: table => new
                {
                    BatchesId = table.Column<int>(type: "int", nullable: false),
                    IngredientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchesIngredients", x => new { x.BatchesId, x.IngredientsId });
                    table.ForeignKey(
                        name: "FK_BatchesIngredients_Batches_BatchesId",
                        column: x => x.BatchesId,
                        principalTable: "Batches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BatchesIngredients_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatchesIngredients_IngredientsId",
                table: "BatchesIngredients",
                column: "IngredientsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatchesIngredients");

            migrationBuilder.AddColumn<int>(
                name: "BatchId",
                table: "Ingredients",
                type: "int",
                nullable: true);

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
                name: "IX_Ingredients_BatchId",
                table: "Ingredients",
                column: "BatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Batches_BatchId",
                table: "Ingredients",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
