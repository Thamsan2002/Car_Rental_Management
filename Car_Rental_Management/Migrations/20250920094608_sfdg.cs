using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_Rental_Management.Migrations
{
    /// <inheritdoc />
    public partial class sfdg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "RoadsideRequests");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "RoadsideRequests",
                newName: "RequestDate");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "RoadsideRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestDate",
                table: "RoadsideRequests",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "RoadsideRequests",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "RoadsideRequests",
                type: "datetime2",
                nullable: true);
        }
    }
}
