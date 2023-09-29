using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarListApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultRolesAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3A484B50-B144-4DC6-9D7E-57FD90C8FD1D", null, "Administrator", "ADMINISTRATOR" },
                    { "7BF4A632-C22F-4053-A1B5-B60AA823EB5D", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "40ACB6F7-4406-4044-9E8C-EFE106A12588", 0, "9f3c3d85-57a0-4e2a-82e1-530d68352b75", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEP3uuPLwD3JLBSKX0PQEcY30a2UlMHyLgMD8q04dNBOGNK4lWy+VW+gtorqWRtkJsg==", null, false, "fb0f4d20-5274-4767-9848-8803407b7e0b", false, "admin@localhost.com" },
                    { "F280DF35-0E50-4290-B92A-069FCD9015E5", 0, "976a239d-e1a1-42fb-88d0-793acd977608", "user@localhost.com", true, false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAELz7xS+lt6dBxHPNe46NzGkobeVUPfKQxHQGxgftl7meQSK1VF7cMg1YJv6ADff2EQ==", null, false, "2b8428ca-059a-4713-a094-2ed04c24ccac", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "3A484B50-B144-4DC6-9D7E-57FD90C8FD1D", "40ACB6F7-4406-4044-9E8C-EFE106A12588" },
                    { "7BF4A632-C22F-4053-A1B5-B60AA823EB5D", "F280DF35-0E50-4290-B92A-069FCD9015E5" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3A484B50-B144-4DC6-9D7E-57FD90C8FD1D", "40ACB6F7-4406-4044-9E8C-EFE106A12588" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7BF4A632-C22F-4053-A1B5-B60AA823EB5D", "F280DF35-0E50-4290-B92A-069FCD9015E5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3A484B50-B144-4DC6-9D7E-57FD90C8FD1D");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7BF4A632-C22F-4053-A1B5-B60AA823EB5D");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "40ACB6F7-4406-4044-9E8C-EFE106A12588");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "F280DF35-0E50-4290-B92A-069FCD9015E5");
        }
    }
}
