using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpTracker.DataAccess.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class user_registration_date : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Registered",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Registered",
                table: "users");
        }
    }
}
