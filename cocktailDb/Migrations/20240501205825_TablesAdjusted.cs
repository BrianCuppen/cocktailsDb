using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cocktailDb.Migrations
{
    /// <inheritdoc />
    public partial class TablesAdjusted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Measurements",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Ingredients",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Ingredients");
        }
    }
}
