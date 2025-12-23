using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrinkManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drinks_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 23, 3, 40, 53, 375, DateTimeKind.Utc).AddTicks(4046), "各类茶饮料", "茶饮" },
                    { 2, new DateTime(2025, 12, 23, 3, 40, 53, 375, DateTimeKind.Utc).AddTicks(4049), "咖啡类饮品", "咖啡" },
                    { 3, new DateTime(2025, 12, 23, 3, 40, 53, 375, DateTimeKind.Utc).AddTicks(4051), "鲜榨果汁", "果汁" },
                    { 4, new DateTime(2025, 12, 23, 3, 40, 53, 375, DateTimeKind.Utc).AddTicks(4052), "奶茶系列", "奶茶" }
                });

            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "Name", "Price", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 12, 23, 3, 40, 53, 375, DateTimeKind.Utc).AddTicks(4183), "清新绿茶", null, "绿茶", 8.00m, 100, new DateTime(2025, 12, 23, 3, 40, 53, 375, DateTimeKind.Utc).AddTicks(4183) },
                    { 2, 1, new DateTime(2025, 12, 23, 3, 40, 53, 375, DateTimeKind.Utc).AddTicks(4189), "经典红茶", null, "红茶", 8.00m, 100, new DateTime(2025, 12, 23, 3, 40, 53, 375, DateTimeKind.Utc).AddTicks(4189) },
                    { 3, 2, new DateTime(2025, 12, 23, 3, 40, 53, 375, DateTimeKind.Utc).AddTicks(4191), "浓郁美式", null, "美式咖啡", 15.00m, 80, new DateTime(2025, 12, 23, 3, 40, 53, 375, DateTimeKind.Utc).AddTicks(4191) },
                    { 4, 2, new DateTime(2025, 12, 23, 3, 40, 53, 375, DateTimeKind.Utc).AddTicks(4193), "香醇拿铁", null, "拿铁", 18.00m, 80, new DateTime(2025, 12, 23, 3, 40, 53, 375, DateTimeKind.Utc).AddTicks(4193) },
                    { 5, 3, new DateTime(2025, 12, 23, 3, 40, 53, 375, DateTimeKind.Utc).AddTicks(4194), "鲜榨橙汁", null, "橙汁", 12.00m, 60, new DateTime(2025, 12, 23, 3, 40, 53, 375, DateTimeKind.Utc).AddTicks(4195) },
                    { 6, 4, new DateTime(2025, 12, 23, 3, 40, 53, 375, DateTimeKind.Utc).AddTicks(4196), "经典珍珠奶茶", null, "珍珠奶茶", 10.00m, 90, new DateTime(2025, 12, 23, 3, 40, 53, 375, DateTimeKind.Utc).AddTicks(4196) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_CategoryId",
                table: "Drinks",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drinks");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
