using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CandidateManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCentreAndCentreCandidateRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("0ebf2035-e23e-421a-9c89-06ac9887891b"));

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("22bd08f6-890f-401b-b415-4a6a64628612"));

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("49b60ef5-c5ac-46a0-a6f3-fd322e0fc005"));

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("605f0f3a-6e0c-4988-baf0-a26662ce5e07"));

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("8bf04c27-1139-43ce-933c-724c9ba967ab"));

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("95248028-958e-467d-9a44-d22105ee7d15"));

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("9dd31215-c6d4-4914-aaa6-bb3b89cc4266"));

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("b792c860-0abe-476f-8b4d-62cd60bc1ee4"));

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("c22c1268-5058-4a5c-ba95-5652adc0568b"));

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("fea2e219-d087-46b4-a0fc-84d3a4236b38"));

            migrationBuilder.AddColumn<Guid>(
                name: "CentreID",
                table: "Candidates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Centres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Certified = table.Column<bool>(type: "bit", nullable: false),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centres", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_CentreID",
                table: "Candidates",
                column: "CentreID");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Centres_CentreID",
                table: "Candidates",
                column: "CentreID",
                principalTable: "Centres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Centres_CentreID",
                table: "Candidates");

            migrationBuilder.DropTable(
                name: "Centres");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_CentreID",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CentreID",
                table: "Candidates");

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "DateOfBirth", "Email", "Forename", "Surname", "SwqrNumber", "TelephoneNumber" },
                values: new object[,]
                {
                    { new Guid("0ebf2035-e23e-421a-9c89-06ac9887891b"), new DateTime(1989, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "james.taylor@example.com", "James", "Taylor", "10190123", "1117896543" },
                    { new Guid("22bd08f6-890f-401b-b415-4a6a64628612"), new DateTime(1996, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "olivia.harris@example.com", "Olivia", "Harris", "10201234", "9991239876" },
                    { new Guid("49b60ef5-c5ac-46a0-a6f3-fd322e0fc005"), new DateTime(1995, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "robert.brown@example.com", "Robert", "Brown", "10056789", "5559876543" },
                    { new Guid("605f0f3a-6e0c-4988-baf0-a26662ce5e07"), new DateTime(1992, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "michael.johnson@example.com", "Michael", "Johnson", "10034567", "9876543210" },
                    { new Guid("8bf04c27-1139-43ce-933c-724c9ba967ab"), new DateTime(1993, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "laura.anderson@example.com", "Laura", "Anderson", "10089012", "2226549876" },
                    { new Guid("95248028-958e-467d-9a44-d22105ee7d15"), new DateTime(1987, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "david.martinez@example.com", "David", "Martinez", "10078901", "3335671234" },
                    { new Guid("9dd31215-c6d4-4914-aaa6-bb3b89cc4266"), new DateTime(1990, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.smith@example.com", "John", "Smith", "10012345", "0987654321" },
                    { new Guid("b792c860-0abe-476f-8b4d-62cd60bc1ee4"), new DateTime(1988, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.davis@example.com", "Emily", "Davis", "10045678", "5551234567" },
                    { new Guid("c22c1268-5058-4a5c-ba95-5652adc0568b"), new DateTime(1985, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.doe@example.com", "Jane", "Doe", "10023456", "1234567890" },
                    { new Guid("fea2e219-d087-46b4-a0fc-84d3a4236b38"), new DateTime(1990, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "sarah.wilson@example.com", "Sarah", "Wilson", "10067890", "4443217890" }
                });
        }
    }
}
