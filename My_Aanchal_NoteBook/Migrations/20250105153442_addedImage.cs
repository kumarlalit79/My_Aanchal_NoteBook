using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_Aanchal_NoteBook.Migrations
{
    /// <inheritdoc />
    public partial class addedImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "MilkEntries",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "MilkEntries");
        }
    }
}
