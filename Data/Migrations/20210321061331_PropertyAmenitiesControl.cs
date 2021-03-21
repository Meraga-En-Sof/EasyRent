using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyRent.Data.Migrations
{
    public partial class PropertyAmenitiesControl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "PropertyAmenities");

            migrationBuilder.AddColumn<int>(
                name: "AmenitiesId",
                table: "PropertyAmenities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OurServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurServices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAmenities_AmenitiesId",
                table: "PropertyAmenities",
                column: "AmenitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyAmenities_Amenities_AmenitiesId",
                table: "PropertyAmenities",
                column: "AmenitiesId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyAmenities_Amenities_AmenitiesId",
                table: "PropertyAmenities");

            migrationBuilder.DropTable(
                name: "OurServices");

            migrationBuilder.DropIndex(
                name: "IX_PropertyAmenities_AmenitiesId",
                table: "PropertyAmenities");

            migrationBuilder.DropColumn(
                name: "AmenitiesId",
                table: "PropertyAmenities");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PropertyAmenities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
