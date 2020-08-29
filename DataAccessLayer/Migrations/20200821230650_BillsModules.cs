using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class BillsModules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillTypeId = table.Column<int>(nullable: false),
                    PurchasePrice = table.Column<decimal>(nullable: true),
                    PurchaseTime = table.Column<DateTime>(nullable: true),
                    SalePrice = table.Column<decimal>(nullable: true),
                    SaleTime = table.Column<DateTime>(nullable: true),
                    ClientName = table.Column<string>(nullable: true),
                    StoreId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Bills_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ItemId",
                table: "Bills",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_StoreId",
                table: "Bills",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");
        }
    }
}
