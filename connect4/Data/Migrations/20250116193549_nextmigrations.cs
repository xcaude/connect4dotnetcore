using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace connect4.Data.Migrations
{
    /// <inheritdoc />
    public partial class nextmigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Games");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Login", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "9c1084fb-7766-4312-8944-a272a7c630de", "Player", "player1@example.com", false, false, null, "player", "PLAYER1@EXAMPLE.COM", "PLAYER1", "password", "AQAAAAIAAYagAAAAEMR1mX+6i/9BdPiElumhxAIGx6KlUADdzStVgirzwjCHnZObfgbHdtqWNOMtnxOAZg==", null, false, "a0d87e12-7d5f-4632-aed1-3ad1f61c2cab", false, "player1" },
                    { "2", 0, "92971c77-96fa-4c23-aa5c-a7aaf12d963c", "Player", "player2@example.com", false, false, null, "player", "PLAYER2@EXAMPLE.COM", "PLAYER2", "password", "AQAAAAIAAYagAAAAEL6dOw308/lwxx+HcU6H4XZPYZ/p09Vkvl77twWZBF9bsPCl3W+Cgg7L5d3gzTjSww==", null, false, "18df1615-ba49-4974-8e5d-286dd9fd964b", false, "player2" },
                    { "3", 0, "ab1c83d9-57fc-4743-b9dc-b5be4ee57701", "Player", "player3@example.com", false, false, null, "player", "PLAYER3@EXAMPLE.COM", "PLAYER3", "password", "AQAAAAIAAYagAAAAEOrdn7fejCFYe/qPq3U/HP4pOgs86fxoai0n6ZcLLJS4Q4QVPFIJYjnCWDxN0jwwHA==", null, false, "7c4a43b2-bb88-4967-9c8c-67b51eadfdf8", false, "player3" }
                });
        }
    }
}
