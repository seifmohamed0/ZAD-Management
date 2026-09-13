using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using ZAD_Management.Infrastructure.Persistence;

#nullable disable

namespace ZAD_Management.Infrastructure.Persistence.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20260913010000_RequireVehicleFileNumber")]
public partial class RequireVehicleFileNumber : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("UPDATE [RentalContracts] SET [VehicleFileNo] = N'' WHERE [VehicleFileNo] IS NULL;");

        migrationBuilder.AlterColumn<string>(
            name: "VehicleFileNo",
            table: "RentalContracts",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(50)",
            oldMaxLength: 50,
            oldNullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "VehicleFileNo",
            table: "RentalContracts",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(50)",
            oldMaxLength: 50);
    }
}
