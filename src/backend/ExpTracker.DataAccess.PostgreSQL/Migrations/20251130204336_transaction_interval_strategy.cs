using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpTracker.DataAccess.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class transaction_interval_strategy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "interval_strategy",
                table: "transactions",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "interval_strategy",
                table: "transactions");
        }
    }
}
