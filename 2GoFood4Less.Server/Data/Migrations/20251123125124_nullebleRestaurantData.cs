using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace _2GoFood4Less.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class nullebleRestaurantData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "OpenTime",
                table: "Restaurants",
                type: "time",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ClosingTime",
                table: "Restaurants",
                type: "time",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenTime",
                table: "Restaurants",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClosingTime",
                table: "Restaurants",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);
        }
    }
}
