using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcesss.Migrations
{
    public partial class Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripeSessionId",
                table: "RoomOrderDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HotelAmenities");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "HotelAmenities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "HotelAmenities");

            migrationBuilder.DropColumn(
                name: "Timming",
                table: "HotelAmenities");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "HotelAmenities");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "HotelAmenities");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "RoomOrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlaceType",
                table: "HotelRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "HotelRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "HotelRoomImages",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HotelAmenities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IconStyle",
                table: "HotelAmenities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

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
                name: "IX_RoomOrderDetails_UserId1",
                table: "RoomOrderDetails",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_UserId",
                table: "HotelRooms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoomImages_UserId",
                table: "HotelRoomImages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelAmenityHotelRoom_HotelRoomsId",
                table: "HotelAmenityHotelRoom",
                column: "HotelRoomsId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRoomImages_ApplicationUser_UserId",
                table: "HotelRoomImages",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRooms_ApplicationUser_UserId",
                table: "HotelRooms",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomOrderDetails_ApplicationUser_UserId1",
                table: "RoomOrderDetails",
                column: "UserId1",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRoomImages_ApplicationUser_UserId",
                table: "HotelRoomImages");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelRooms_ApplicationUser_UserId",
                table: "HotelRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomOrderDetails_ApplicationUser_UserId1",
                table: "RoomOrderDetails");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "HotelAmenityHotelRoom");

            migrationBuilder.DropIndex(
                name: "IX_RoomOrderDetails_UserId1",
                table: "RoomOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_HotelRooms_UserId",
                table: "HotelRooms");

            migrationBuilder.DropIndex(
                name: "IX_HotelRoomImages_UserId",
                table: "HotelRoomImages");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "RoomOrderDetails");

            migrationBuilder.DropColumn(
                name: "PlaceType",
                table: "HotelRooms");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HotelRooms");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HotelRoomImages");

            migrationBuilder.AddColumn<string>(
                name: "StripeSessionId",
                table: "RoomOrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HotelAmenities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IconStyle",
                table: "HotelAmenities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "HotelAmenities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "HotelAmenities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "HotelAmenities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Timming",
                table: "HotelAmenities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "HotelAmenities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "HotelAmenities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
