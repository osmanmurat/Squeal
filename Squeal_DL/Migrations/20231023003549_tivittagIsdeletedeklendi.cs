using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Squeal_DL.Migrations
{
    /// <inheritdoc />
    public partial class tivittagIsdeletedeklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TivitTagTable",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TivitTagTable");
        }
    }
}
