using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.Migrations.PlansDb
{
    /// <inheritdoc />
    public partial class UpdatePlanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlanName = table.Column<string>(type: "TEXT", nullable: false),
                    Scenario = table.Column<string>(type: "TEXT", nullable: false),
                    Shelter = table.Column<string>(type: "TEXT", nullable: false),
                    FoodSources = table.Column<string>(type: "TEXT", nullable: false),
                    WaterSources = table.Column<string>(type: "TEXT", nullable: false),
                    Weapons = table.Column<string>(type: "TEXT", nullable: false),
                    Vehicles = table.Column<string>(type: "TEXT", nullable: false),
                    Fuel = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plans");
        }
    }
}
