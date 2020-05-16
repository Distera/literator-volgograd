using Microsoft.EntityFrameworkCore.Migrations;

namespace LiteratorVolgograd.Migrations
{
    public partial class AddAdminRoleAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6f6f6191-bafc-4f12-bf9a-e55f535de420", "e55a11ba-5a94-461b-8b66-0f1de45b2e8d", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9288117a-2d5a-4baa-aed3-20d64a824edc", 0, "39cd1e02-ad3c-488e-bc65-fd7a89c9aa75", "admin@email.com", true, false, null, "ADMIN@EMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEFat+RECT4bwlNGfEbXguHQKLWwNLSSiWPgQUotLUS2Avw2mlOnadsZqV0e7q6fAxg==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "9288117a-2d5a-4baa-aed3-20d64a824edc", "6f6f6191-bafc-4f12-bf9a-e55f535de420" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "9288117a-2d5a-4baa-aed3-20d64a824edc", "6f6f6191-bafc-4f12-bf9a-e55f535de420" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "6f6f6191-bafc-4f12-bf9a-e55f535de420", "e55a11ba-5a94-461b-8b66-0f1de45b2e8d" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "9288117a-2d5a-4baa-aed3-20d64a824edc", "39cd1e02-ad3c-488e-bc65-fd7a89c9aa75" });
        }
    }
}
