using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRecipeBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Recipe name"),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "Recipe description"),
                    PreparationTime = table.Column<int>(type: "integer", nullable: false, comment: "Preparation time in minutes"),
                    CookingTime = table.Column<int>(type: "integer", nullable: false, comment: "Cooking time in minutes"),
                    NumberOfServings = table.Column<int>(type: "integer", nullable: false, comment: "Number of servings"),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
