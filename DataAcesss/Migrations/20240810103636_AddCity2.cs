using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcesss.Migrations
{
    public partial class AddCity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRooms_Cities_CityHotelId",
                table: "HotelRooms");

            migrationBuilder.AlterColumn<int>(
                name: "CityHotelId",
                table: "HotelRooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRooms_Cities_CityHotelId",
                table: "HotelRooms",
                column: "CityHotelId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRooms_Cities_CityHotelId",
                table: "HotelRooms");

            migrationBuilder.AlterColumn<int>(
                name: "CityHotelId",
                table: "HotelRooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRooms_Cities_CityHotelId",
                table: "HotelRooms",
                column: "CityHotelId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
