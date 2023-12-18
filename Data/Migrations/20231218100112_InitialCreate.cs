using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerManagementService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    AuthId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    Funds = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Town = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    County = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Customers",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AuthId" },
                values: new object[,]
                {
                    { new Guid("a3b8eda4-faf1-4997-94de-d67305ed750e"), "auth0|60a0a0a0a0a0a0a0a0a0a0a1" },
                    { new Guid("be2efcdc-c19c-4af7-91fb-8cdf42c6480e"), "auth0|60a0a0a0a0a0a0a0a0a0a0a0" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CustomerId", "Funds" },
                values: new object[,]
                {
                    { new Guid("596a149f-1752-4c9b-bbad-cd69e909f44c"), new Guid("be2efcdc-c19c-4af7-91fb-8cdf42c6480e"), 100m },
                    { new Guid("85f72b1a-ef80-4198-bdca-740ced5e6335"), new Guid("a3b8eda4-faf1-4997-94de-d67305ed750e"), 200m }
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "Address", "County", "CustomerId", "Email", "Name", "Phone", "Postcode", "Town" },
                values: new object[,]
                {
                    { new Guid("41f2a643-9dc4-4480-ab0a-7b44ff8a189a"), "2 High Street", "Anyshire", new Guid("a3b8eda4-faf1-4997-94de-d67305ed750e"), "jdoe~gmail.com", "Jane Doe", "01234 567890", "AB1 2CD", "Anytown" },
                    { new Guid("7d644907-37a5-4fb5-99c5-6b662114dbe1"), "1 High Street", "Anyshire", new Guid("be2efcdc-c19c-4af7-91fb-8cdf42c6480e"), "jsmith@gmail.com", "John Smith", "01234 567890", "AB1 2CD", "Anytown" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerId",
                table: "Accounts",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_CustomerId",
                table: "Profiles",
                column: "CustomerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
