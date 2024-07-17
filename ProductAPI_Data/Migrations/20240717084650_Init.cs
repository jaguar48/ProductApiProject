using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductAPI_Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a2429bb-23c0-49fc-a122-b441b150c99d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f3174d6d-08ed-4020-ab8d-6867c9de7d9e", null, "Seller", "SELLER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3174d6d-08ed-4020-ab8d-6867c9de7d9e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0a2429bb-23c0-49fc-a122-b441b150c99d", null, "Seller", "SELLER" });
        }
    }
}
