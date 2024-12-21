using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Aanchal_NoteBook.Migrations
{
    /// <inheritdoc />
    public partial class CodeAndOtherPropertyAddedd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DairyLocation",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DairyName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SachiveName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DairyLocation",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DairyName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SachiveName",
                table: "Users");
        }
    }
}
