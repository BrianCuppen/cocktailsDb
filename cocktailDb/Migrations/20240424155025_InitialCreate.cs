using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace cocktailDb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EmailAdress = table.Column<string>(type: "longtext", nullable: true),
                    EmailsSent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Glasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Glasses", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DbDrinkId = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    AlternateName = table.Column<string>(type: "longtext", nullable: true),
                    Category = table.Column<string>(type: "longtext", nullable: true),
                    Iba = table.Column<string>(type: "longtext", nullable: true),
                    Alcoholic = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    GlassTypeId = table.Column<int>(type: "int", nullable: true),
                    Instructions = table.Column<string>(type: "longtext", nullable: true),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: true),
                    IsEdited = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drinks_Glasses_GlassTypeId",
                        column: x => x.GlassTypeId,
                        principalTable: "Glasses",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdOfDrink = table.Column<int>(type: "int", nullable: false),
                    DrinkName = table.Column<string>(type: "longtext", nullable: true),
                    Ingredient1 = table.Column<string>(type: "longtext", nullable: true),
                    Ingredient2 = table.Column<string>(type: "longtext", nullable: true),
                    Ingredient3 = table.Column<string>(type: "longtext", nullable: true),
                    Ingredient4 = table.Column<string>(type: "longtext", nullable: true),
                    Ingredient5 = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Drinks_IdOfDrink",
                        column: x => x.IdOfDrink,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdOfDrink = table.Column<int>(type: "int", nullable: false),
                    DrinkName = table.Column<string>(type: "longtext", nullable: true),
                    Measure1 = table.Column<string>(type: "longtext", nullable: true),
                    Measure2 = table.Column<string>(type: "longtext", nullable: true),
                    Measure3 = table.Column<string>(type: "longtext", nullable: true),
                    Measure4 = table.Column<string>(type: "longtext", nullable: true),
                    Measure5 = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurements_Drinks_IdOfDrink",
                        column: x => x.IdOfDrink,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_GlassTypeId",
                table: "Drinks",
                column: "GlassTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_IdOfDrink",
                table: "Ingredients",
                column: "IdOfDrink",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_IdOfDrink",
                table: "Measurements",
                column: "IdOfDrink",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Drinks");

            migrationBuilder.DropTable(
                name: "Glasses");
        }
    }
}
