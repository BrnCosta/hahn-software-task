using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HahnAssessmentTask.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInfoDateTimeToDateOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockDailyInformation_Stocks_StockSymbol",
                table: "StockDailyInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockDailyInformation",
                table: "StockDailyInformation");

            migrationBuilder.RenameTable(
                name: "StockDailyInformation",
                newName: "StockDailyInformations");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "StockDailyInformations",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockDailyInformations",
                table: "StockDailyInformations",
                columns: new[] { "StockSymbol", "Date" });

            migrationBuilder.AddForeignKey(
                name: "FK_StockDailyInformations_Stocks_StockSymbol",
                table: "StockDailyInformations",
                column: "StockSymbol",
                principalTable: "Stocks",
                principalColumn: "Symbol",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockDailyInformations_Stocks_StockSymbol",
                table: "StockDailyInformations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockDailyInformations",
                table: "StockDailyInformations");

            migrationBuilder.RenameTable(
                name: "StockDailyInformations",
                newName: "StockDailyInformation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "StockDailyInformation",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockDailyInformation",
                table: "StockDailyInformation",
                columns: new[] { "StockSymbol", "Date" });

            migrationBuilder.AddForeignKey(
                name: "FK_StockDailyInformation_Stocks_StockSymbol",
                table: "StockDailyInformation",
                column: "StockSymbol",
                principalTable: "Stocks",
                principalColumn: "Symbol",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
