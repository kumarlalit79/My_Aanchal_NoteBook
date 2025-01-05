using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Aanchal_NoteBook.Migrations
{
    /// <inheritdoc />
    public partial class AddeddScan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "MilkEntries");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "MilkEntries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "MilkEntries",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "MilkEntries",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
