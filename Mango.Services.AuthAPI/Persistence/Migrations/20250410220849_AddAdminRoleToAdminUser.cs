using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Services.AuthAPI.Persistence
{
    /// <inheritdoc />
    public partial class AddAdminRoleToAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "72134477-28b4-4430-9c4a-70422855560b", "bf301080-df71-4f1f-a9dc-e3b72c0af129" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "72134477-28b4-4430-9c4a-70422855560b", "bf301080-df71-4f1f-a9dc-e3b72c0af129" });
        }
    }
}
