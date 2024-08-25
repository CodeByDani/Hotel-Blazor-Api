using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcesss.Migrations
{
    public partial class Change6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoomHotelAmenity_HotelAmenities_HotelAmenityId",
                table: "HotelRoomHotelAmenity");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoomHotelAmenity_HotelRooms_HotelRoomId",
                table: "HotelRoomHotelAmenity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelRoomHotelAmenity",
                table: "HotelRoomHotelAmenity");

            migrationBuilder.RenameTable(
                name: "HotelRoomHotelAmenity",
                newName: "HotelRoomHotelAmenities");

            migrationBuilder.RenameIndex(
                name: "IX_HotelRoomHotelAmenity_HotelRoomId",
                table: "HotelRoomHotelAmenities",
                newName: "IX_HotelRoomHotelAmenities_HotelRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelRoomHotelAmenity_HotelAmenityId",
                table: "HotelRoomHotelAmenities",
                newName: "IX_HotelRoomHotelAmenities_HotelAmenityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelRoomHotelAmenities",
                table: "HotelRoomHotelAmenities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoomHotelAmenities_HotelAmenities_HotelAmenityId",
                table: "HotelRoomHotelAmenities",
                column: "HotelAmenityId",
                principalTable: "HotelAmenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoomHotelAmenities_HotelRooms_HotelRoomId",
                table: "HotelRoomHotelAmenities",
                column: "HotelRoomId",
                principalTable: "HotelRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoomHotelAmenities_HotelAmenities_HotelAmenityId",
                table: "HotelRoomHotelAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoomHotelAmenities_HotelRooms_HotelRoomId",
                table: "HotelRoomHotelAmenities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelRoomHotelAmenities",
                table: "HotelRoomHotelAmenities");

            migrationBuilder.RenameTable(
                name: "HotelRoomHotelAmenities",
                newName: "HotelRoomHotelAmenity");

            migrationBuilder.RenameIndex(
                name: "IX_HotelRoomHotelAmenities_HotelRoomId",
                table: "HotelRoomHotelAmenity",
                newName: "IX_HotelRoomHotelAmenity_HotelRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelRoomHotelAmenities_HotelAmenityId",
                table: "HotelRoomHotelAmenity",
                newName: "IX_HotelRoomHotelAmenity_HotelAmenityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelRoomHotelAmenity",
                table: "HotelRoomHotelAmenity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoomHotelAmenity_HotelAmenities_HotelAmenityId",
                table: "HotelRoomHotelAmenity",
                column: "HotelAmenityId",
                principalTable: "HotelAmenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoomHotelAmenity_HotelRooms_HotelRoomId",
                table: "HotelRoomHotelAmenity",
                column: "HotelRoomId",
                principalTable: "HotelRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
