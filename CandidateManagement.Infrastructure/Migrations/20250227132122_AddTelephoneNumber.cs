using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTelephoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TelephoneNumber",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelephoneNumber",
                table: "Candidates");
        }
    }
}
