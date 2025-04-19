using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.Migrations.PlansDb
{
    /// <inheritdoc />
    public partial class AddYourMigrationNameHere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Plans",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<decimal>(
                name: "Budget",
                table: "Plans",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Plans",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Plans");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Plans",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
