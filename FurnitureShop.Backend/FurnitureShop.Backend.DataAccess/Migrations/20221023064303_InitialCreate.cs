using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShop.Backend.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buyer",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NumberPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyer", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Furniture",
                columns: table => new
                {
                    Model = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Characteristics = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furniture", x => x.Model);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Number = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyerCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Number);
                    table.ForeignKey(
                        name: "FK_Contract_Buyer_BuyerCode",
                        column: x => x.BuyerCode,
                        principalTable: "Buyer",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    ContractNumber = table.Column<int>(type: "int", nullable: false),
                    FurnitureModel = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => new { x.ContractNumber, x.FurnitureModel });
                    table.ForeignKey(
                        name: "FK_Sale_Contract_ContractNumber",
                        column: x => x.ContractNumber,
                        principalTable: "Contract",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sale_Furniture_FurnitureModel",
                        column: x => x.FurnitureModel,
                        principalTable: "Furniture",
                        principalColumn: "Model",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buyer_NumberPhone",
                table: "Buyer",
                column: "NumberPhone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_BuyerCode",
                table: "Contract",
                column: "BuyerCode");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_FurnitureModel",
                table: "Sale",
                column: "FurnitureModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Furniture");

            migrationBuilder.DropTable(
                name: "Buyer");
        }
    }
}
