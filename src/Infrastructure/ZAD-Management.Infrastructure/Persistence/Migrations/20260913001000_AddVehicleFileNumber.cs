using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using ZAD_Management.Infrastructure.Persistence;

#nullable disable

namespace ZAD_Management.Infrastructure.Persistence.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20260913001000_AddVehicleFileNumber")]
public partial class AddVehicleFileNumber : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("IF COL_LENGTH('RentalContracts', 'VehicleFileNo') IS NULL ALTER TABLE [RentalContracts] ADD [VehicleFileNo] nvarchar(50) NULL;");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("IF COL_LENGTH('RentalContracts', 'VehicleFileNo') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [VehicleFileNo];");
    }
}
