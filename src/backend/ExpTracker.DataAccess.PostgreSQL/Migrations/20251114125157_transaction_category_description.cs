using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpTracker.DataAccess.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class transaction_category_description : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "transaction_categories",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "transaction_categories");
        }
    }
}
