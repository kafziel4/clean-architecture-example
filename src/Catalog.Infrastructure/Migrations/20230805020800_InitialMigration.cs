using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "material.jpg", "Material Escolar" },
                    { 2, "eletronicos.jpg", "Eletrônicos" },
                    { 3, "acessorios.jpg", "Acessórios" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "RegistrationDate", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "Caneta esferográfica", "caneta.jpg", "Caneta", 2.00m, new DateTime(2023, 8, 4, 23, 8, 0, 852, DateTimeKind.Local).AddTicks(842), 50 },
                    { 2, 1, "Lápis HB", "lapis.jpg", "Lápis", 1.00m, new DateTime(2023, 8, 4, 23, 8, 0, 852, DateTimeKind.Local).AddTicks(900), 70 },
                    { 3, 1, "Borracha branca", "borracha.jpg", "Borracha", 1.50m, new DateTime(2023, 8, 4, 23, 8, 0, 852, DateTimeKind.Local).AddTicks(908), 30 },
                    { 4, 2, "Notebook 15 polegadas", "notebook.jpg", "Notebook", 3000.00m, new DateTime(2023, 8, 4, 23, 8, 0, 852, DateTimeKind.Local).AddTicks(914), 5 },
                    { 5, 2, "Tablet 10 polegadas", "tablet.jpg", "Tablet", 2500.00m, new DateTime(2023, 8, 4, 23, 8, 0, 852, DateTimeKind.Local).AddTicks(919), 10 },
                    { 6, 2, "Celular 5 polegadas", "celular.jpg", "Celular", 1500.00m, new DateTime(2023, 8, 4, 23, 8, 0, 852, DateTimeKind.Local).AddTicks(925), 15 },
                    { 7, 3, "Bolsa de couro", "bolsa.jpg", "Bolsa", 500.00m, new DateTime(2023, 8, 4, 23, 8, 0, 852, DateTimeKind.Local).AddTicks(930), 20 },
                    { 8, 3, "Carteira de couro", "carteira.jpg", "Carteira", 700.00m, new DateTime(2023, 8, 4, 23, 8, 0, 852, DateTimeKind.Local).AddTicks(937), 40 },
                    { 9, 3, "Cinto de couro", "cinto.jpg", "Cinto", 400.00m, new DateTime(2023, 8, 4, 23, 8, 0, 852, DateTimeKind.Local).AddTicks(942), 60 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
