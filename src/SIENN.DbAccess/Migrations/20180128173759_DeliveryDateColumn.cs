using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SIENN.DbAccess.Migrations
{
    public partial class DeliveryDateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProductsToCategories_Id",
                table: "ProductsToCategories");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextDelivery",
                table: "Products",
                type: "timestamp",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextDelivery",
                table: "Products");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProductsToCategories_Id",
                table: "ProductsToCategories",
                column: "Id");
        }
    }
}
