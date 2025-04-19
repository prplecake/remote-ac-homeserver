using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RemoteAc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Mac_SensorClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mac",
                table: "SensorClients",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mac",
                table: "SensorClients");
        }
    }
}
