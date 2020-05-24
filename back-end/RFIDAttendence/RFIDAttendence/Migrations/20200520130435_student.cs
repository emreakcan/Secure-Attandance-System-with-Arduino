using Microsoft.EntityFrameworkCore.Migrations;

namespace RFIDAttendence.Migrations
{
    public partial class student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Attendence",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Attendence_StudentId",
                table: "Attendence",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendence_Student_StudentId",
                table: "Attendence",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendence_Student_StudentId",
                table: "Attendence");

            migrationBuilder.DropIndex(
                name: "IX_Attendence_StudentId",
                table: "Attendence");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Attendence");
        }
    }
}
