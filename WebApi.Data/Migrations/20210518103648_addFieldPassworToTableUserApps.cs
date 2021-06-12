using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Data.Migrations
{
    public partial class addFieldPassworToTableUserApps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("adea5474-ac7c-46e3-81d2-181f7636f389"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("adea5474-ac7c-46e3-81d2-181f7636f389"), new Guid("136fe0ed-c020-4376-9308-bd3eedd86473") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("136fe0ed-c020-4376-9308-bd3eedd86473"));

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AppUsers",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "AppUsers");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("adea5474-ac7c-46e3-81d2-181f7636f389"), "91ef6546-b93f-4543-8c2b-d7902943b7d2", "Admintrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("adea5474-ac7c-46e3-81d2-181f7636f389"), new Guid("136fe0ed-c020-4376-9308-bd3eedd86473") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("136fe0ed-c020-4376-9308-bd3eedd86473"), 0, "af07a2ba-3c83-41d1-974c-d4dc4095e15a", new DateTime(1997, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "tacong56@gmail.com", true, "Ta", "Cong", false, null, "tacong56@gmail.com", "admin", "AQAAAAEAACcQAAAAECuip2DCKf2fhvv49XerJJ5IBijutBipfoXgaUsnX1QuZ9I9w4KPXb+8GxVEe5Slog==", null, false, "", false, "admin" });
        }
    }
}
