using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RosterMate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthAndCompanySupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "employmentType",
                table: "EmploymentDetails",
                newName: "EmploymentType");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Staff",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Staff",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Staff",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Staff",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Staff_CompanyId",
                table: "Staff",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Company_CompanyId",
                table: "Staff",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Company_CompanyId",
                table: "Staff");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Staff_CompanyId",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Staff");

            migrationBuilder.RenameColumn(
                name: "EmploymentType",
                table: "EmploymentDetails",
                newName: "employmentType");
        }
    }
}
