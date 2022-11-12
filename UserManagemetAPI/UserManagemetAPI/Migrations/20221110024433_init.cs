using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagemetAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBLDesignations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBLDesignations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TBLEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Doj = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsMarried = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false),
                    DesignationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBLEmployees", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBLDesignations");

            migrationBuilder.DropTable(
                name: "TBLEmployees");
        }
    }
}
