using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("70d2d047-29fe-4dba-a556-e88e515df62d"));

            migrationBuilder.DeleteData(
                table: "Centres",
                keyColumn: "Id",
                keyValue: new Guid("95af1465-dec0-4eae-a38e-48b64293992e"));

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CentreCourse",
                columns: table => new
                {
                    CentresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoursesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentreCourse", x => new { x.CentresId, x.CoursesId });
                    table.ForeignKey(
                        name: "FK_CentreCourse_Centres_CentresId",
                        column: x => x.CentresId,
                        principalTable: "Centres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CentreCourse_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Centres",
                columns: new[] { "Id", "Address", "Certified", "Email", "Name", "TelephoneNumber" },
                values: new object[] { new Guid("9ef4508a-9776-4658-a459-bc97849884a7"), "45 Tech Avenue, Manchester, UK", true, "contact@techskills.com", "Tech Skills Academy", "0161-9876-5432" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("7b71bb7d-2ced-4881-b0a7-2c8e683df4d8"), "Computer Science" });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "CentreID", "DateOfBirth", "Email", "Forename", "Surname", "SwqrNumber", "TelephoneNumber" },
                values: new object[] { new Guid("32470c7d-933f-48e3-bc75-6dee2daf40fe"), new Guid("9ef4508a-9776-4658-a459-bc97849884a7"), new DateTime(1990, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.smith@example.com", "John", "Smith", "10012345", "0987654321" });

            migrationBuilder.CreateIndex(
                name: "IX_CentreCourse_CoursesId",
                table: "CentreCourse",
                column: "CoursesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CentreCourse");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("32470c7d-933f-48e3-bc75-6dee2daf40fe"));

            migrationBuilder.DeleteData(
                table: "Centres",
                keyColumn: "Id",
                keyValue: new Guid("9ef4508a-9776-4658-a459-bc97849884a7"));

            migrationBuilder.InsertData(
                table: "Centres",
                columns: new[] { "Id", "Address", "Certified", "Email", "Name", "TelephoneNumber" },
                values: new object[] { new Guid("95af1465-dec0-4eae-a38e-48b64293992e"), "45 Tech Avenue, Manchester, UK", true, "contact@techskills.com", "Tech Skills Academy", "0161-9876-5432" });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "CentreID", "DateOfBirth", "Email", "Forename", "Surname", "SwqrNumber", "TelephoneNumber" },
                values: new object[] { new Guid("70d2d047-29fe-4dba-a556-e88e515df62d"), new Guid("95af1465-dec0-4eae-a38e-48b64293992e"), new DateTime(1990, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.smith@example.com", "John", "Smith", "10012345", "0987654321" });
        }
    }
}
