using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HahnAssessmentTask.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStockPriceField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Stocks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Stocks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
