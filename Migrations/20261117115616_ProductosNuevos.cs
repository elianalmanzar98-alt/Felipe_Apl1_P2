using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Felipe_Ap1_P2.Migrations
{
    /// <inheritdoc />
    public partial class ProductosNuevos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 1,
                columns: new[] { "Nombre", "Stock" },
                values: new object[] { "Cafe Negro", 100 });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 2,
                columns: new[] { "Nombre", "Stock" },
                values: new object[] { "Cafe Latte", 80 });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 4,
                columns: new[] { "Nombre", "Stock" },
                values: new object[] { "Double Espresso 200 ml", 45 });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 5,
                columns: new[] { "Nombre", "Stock" },
                values: new object[] { "Caramelo Nacchiato", 30 });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 6,
                column: "Stock",
                value: 70);

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "ProductoId", "Nombre", "Precio", "Stock" },
                values: new object[] { 7, "Capuchino", 180.00m, 90 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 1,
                columns: new[] { "Nombre", "Stock" },
                values: new object[] { "Frappuccino Mocha 281 ml", 50 });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 2,
                columns: new[] { "Nombre", "Stock" },
                values: new object[] { "Frappuccino Vainilla 281 ml", 50 });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 4,
                columns: new[] { "Nombre", "Stock" },
                values: new object[] { "Doubleshot Espresso 200 ml", 30 });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 5,
                columns: new[] { "Nombre", "Stock" },
                values: new object[] { "Cold Brew Original 330 ml", 25 });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "ProductoId",
                keyValue: 6,
                column: "Stock",
                value: 60);
        }
    }
}
