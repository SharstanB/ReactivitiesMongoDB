using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class userToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiration",
                table: "AspNetUsers",
                newName: "DeletedAt");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "AspNetUserTokens",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUserTokens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AspNetUserTokens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "AspNetUserTokens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserTokens",
                type: "character varying(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FingerPrint",
                table: "AspNetUserTokens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "AspNetUserTokens",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiration",
                table: "AspNetUserTokens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_AppUserId",
                table: "AspNetUserTokens",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_DeviceId",
                table: "AspNetUserTokens",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_Value",
                table: "AspNetUserTokens",
                column: "Value");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_AppUserId",
                table: "AspNetUserTokens",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_AppUserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserTokens_AppUserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserTokens_DeviceId",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserTokens_Value",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "FingerPrint",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiration",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "AspNetUsers",
                newName: "RefreshTokenExpiration");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }
    }
}
