using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "School");

            migrationBuilder.CreateTable(
                name: "Salutation",
                schema: "School",
                columns: table => new
                {
                    SalutationId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(maxLength: 10, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salutation", x => x.SalutationId);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                schema: "School",
                columns: table => new
                {
                    TeacherId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    SalutationId = table.Column<long>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.TeacherId);
                    table.ForeignKey(
                        name: "FK_Teacher_Salutation_SalutationId",
                        column: x => x.SalutationId,
                        principalSchema: "School",
                        principalTable: "Salutation",
                        principalColumn: "SalutationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                schema: "School",
                columns: table => new
                {
                    ClassId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Location = table.Column<string>(maxLength: 300, nullable: false),
                    TeacherId = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.ClassId);
                    table.ForeignKey(
                        name: "FK_Class_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalSchema: "School",
                        principalTable: "Teacher",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                schema: "School",
                columns: table => new
                {
                    StudentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Age = table.Column<int>(nullable: false),
                    GPA = table.Column<decimal>(type: "decimal(2, 1)", nullable: false),
                    ClassId = table.Column<long>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Student_Class_ClassId",
                        column: x => x.ClassId,
                        principalSchema: "School",
                        principalTable: "Class",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "School",
                table: "Salutation",
                columns: new[] { "SalutationId", "CreatedDate", "Type", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, new DateTime(2020, 1, 25, 22, 16, 12, 257, DateTimeKind.Utc).AddTicks(7817), "Mr", null },
                    { 2L, new DateTime(2020, 1, 25, 22, 16, 12, 257, DateTimeKind.Utc).AddTicks(8640), "Mrs", null },
                    { 3L, new DateTime(2020, 1, 25, 22, 16, 12, 257, DateTimeKind.Utc).AddTicks(8653), "Ms", null },
                    { 4L, new DateTime(2020, 1, 25, 22, 16, 12, 257, DateTimeKind.Utc).AddTicks(8654), "Miss", null },
                    { 5L, new DateTime(2020, 1, 25, 22, 16, 12, 257, DateTimeKind.Utc).AddTicks(8657), "Dr", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Class_TeacherId",
                schema: "School",
                table: "Class",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_ClassId",
                schema: "School",
                table: "Student",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_SalutationId",
                schema: "School",
                table: "Teacher",
                column: "SalutationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student",
                schema: "School");

            migrationBuilder.DropTable(
                name: "Class",
                schema: "School");

            migrationBuilder.DropTable(
                name: "Teacher",
                schema: "School");

            migrationBuilder.DropTable(
                name: "Salutation",
                schema: "School");
        }
    }
}
