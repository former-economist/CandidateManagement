using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCentreAndCandidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
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
                        name: "FK_CentreCourse_Course_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registration_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registration_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Centres",
                columns: new[] { "Id", "Address", "Certified", "Email", "Name", "TelephoneNumber" },
                values: new object[] { new Guid("be769429-abe0-4446-aa60-51a45fe64dc3"), "45 Tech Avenue, Manchester, UK", true, "contact@techskills.com", "Tech Skills Academy", "0161-9876-5432" });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "CentreID", "DateOfBirth", "Email", "Forename", "Surname", "SwqrNumber", "TelephoneNumber" },
                values: new object[] { new Guid("d82d5a9c-4488-4a15-8134-32b2c69b42d4"), new Guid("be769429-abe0-4446-aa60-51a45fe64dc3"), new DateTime(1990, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.smith@example.com", "John", "Smith", "10012345", "0987654321" });

            migrationBuilder.CreateIndex(
                name: "IX_CentreCourse_CoursesId",
                table: "CentreCourse",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_CandidateId",
                table: "Registration",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_CourseId",
                table: "Registration",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CentreCourse");

            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("d82d5a9c-4488-4a15-8134-32b2c69b42d4"));

            migrationBuilder.DeleteData(
                table: "Centres",
                keyColumn: "Id",
                keyValue: new Guid("be769429-abe0-4446-aa60-51a45fe64dc3"));
        }
    }
}
