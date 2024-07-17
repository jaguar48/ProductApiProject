using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductAPI_Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd3767bb-ea1b-4f9e-9df3-ec8f18e1c6ee");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0a2429bb-23c0-49fc-a122-b441b150c99d", null, "Seller", "SELLER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a2429bb-23c0-49fc-a122-b441b150c99d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bd3767bb-ea1b-4f9e-9df3-ec8f18e1c6ee", null, "Seller", "SELLER" });
        }
    }
}
