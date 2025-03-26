using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSetToCoursesInSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("32470c7d-933f-48e3-bc75-6dee2daf40fe"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("7b71bb7d-2ced-4881-b0a7-2c8e683df4d8"));

            migrationBuilder.DeleteData(
                table: "Centres",
                keyColumn: "Id",
                keyValue: new Guid("9ef4508a-9776-4658-a459-bc97849884a7"));

            migrationBuilder.InsertData(
                table: "Centres",
                columns: new[] { "Id", "Address", "Certified", "Email", "Name", "TelephoneNumber" },
                values: new object[] { new Guid("6235434b-df1f-4032-bd25-18f0108f70c0"), "45 Tech Avenue, Manchester, UK", true, "contact@techskills.com", "Tech Skills Academy", "0161-9876-5432" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("a9a47f6c-8531-44be-bf01-6f97c3434ee1"), "Computer Science" });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "CentreID", "DateOfBirth", "Email", "Forename", "Surname", "SwqrNumber", "TelephoneNumber" },
                values: new object[] { new Guid("26f0cd60-c5a2-4e69-b10d-51117894367d"), new Guid("6235434b-df1f-4032-bd25-18f0108f70c0"), new DateTime(1990, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.smith@example.com", "John", "Smith", "10012345", "0987654321" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: new Guid("26f0cd60-c5a2-4e69-b10d-51117894367d"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("a9a47f6c-8531-44be-bf01-6f97c3434ee1"));

            migrationBuilder.DeleteData(
                table: "Centres",
                keyColumn: "Id",
                keyValue: new Guid("6235434b-df1f-4032-bd25-18f0108f70c0"));

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
        }
    }
}
