using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2GoFood4Less.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class LocationOrder1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Orders");
        }
    }
}
