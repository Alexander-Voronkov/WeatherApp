using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "WeatherRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RawJsonData",
                table: "WeatherRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RequestedAt",
                table: "WeatherRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "WeatherRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RegisteredAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_WeatherRequests_UserId",
                table: "WeatherRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherRequests_Users_UserId",
                table: "WeatherRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherRequests_Users_UserId",
                table: "WeatherRequests");

            migrationBuilder.DropIndex(
                name: "IX_WeatherRequests_UserId",
                table: "WeatherRequests");

            migrationBuilder.DropColumn(
                name: "City",
                table: "WeatherRequests");

            migrationBuilder.DropColumn(
                name: "RawJsonData",
                table: "WeatherRequests");

            migrationBuilder.DropColumn(
                name: "RequestedAt",
                table: "WeatherRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WeatherRequests");

            migrationBuilder.DropColumn(
                name: "RegisteredAt",
                table: "Users");
        }
    }
}
