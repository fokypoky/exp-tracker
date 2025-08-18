using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpTracker.DataAccess.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class user_registration_date_extended_configuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Registered",
                table: "users",
                newName: "registered");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "registered",
                table: "users",
                newName: "Registered");
        }
    }
}
