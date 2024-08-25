using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcesss.Migrations
{
    public partial class Change5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelAmenityHotelRoom");

            migrationBuilder.CreateTable(
                name: "HotelRoomHotelAmenity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelRoomId = table.Column<int>(type: "int", nullable: false),
                    HotelAmenityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRoomHotelAmenity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelRoomHotelAmenity_HotelAmenities_HotelAmenityId",
                        column: x => x.HotelAmenityId,
                        principalTable: "HotelAmenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelRoomHotelAmenity_HotelRooms_HotelRoomId",
                        column: x => x.HotelRoomId,
                        principalTable: "HotelRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoomHotelAmenity_HotelAmenityId",
                table: "HotelRoomHotelAmenity",
                column: "HotelAmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoomHotelAmenity_HotelRoomId",
                table: "HotelRoomHotelAmenity",
                column: "HotelRoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelRoomHotelAmenity");

            migrationBuilder.CreateTable(
                name: "HotelAmenityHotelRoom",
                columns: table => new
                {
                    HotelAmenitiesId = table.Column<int>(type: "int", nullable: false),
                    HotelRoomsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelAmenityHotelRoom", x => new { x.HotelAmenitiesId, x.HotelRoomsId });
                    table.ForeignKey(
                        name: "FK_HotelAmenityHotelRoom_HotelAmenities_HotelAmenitiesId",
                        column: x => x.HotelAmenitiesId,
                        principalTable: "HotelAmenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelAmenityHotelRoom_HotelRooms_HotelRoomsId",
                        column: x => x.HotelRoomsId,
                        principalTable: "HotelRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelAmenityHotelRoom_HotelRoomsId",
                table: "HotelAmenityHotelRoom",
                column: "HotelRoomsId");
        }
    }
}
