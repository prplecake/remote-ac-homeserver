﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RemoteAc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddKeyToAppStateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AppState",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppState",
                table: "AppState",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppState",
                table: "AppState");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppState");
        }
    }
}
