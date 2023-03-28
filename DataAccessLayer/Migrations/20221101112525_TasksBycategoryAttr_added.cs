using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class TasksBycategoryAttr_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_SubCategoryId",
                table: "Tasks",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_SubCategories_SubCategoryId",
                table: "Tasks",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "SubCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_SubCategories_SubCategoryId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_SubCategoryId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Tasks");
        }
    }
}
