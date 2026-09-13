using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ZAD_Management.Infrastructure.Persistence;

#nullable disable

namespace ZAD_Management.Infrastructure.Persistence.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20260913000000_AddMaintenanceDoneByTenant")]
public partial class AddMaintenanceDoneByTenant : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("IF COL_LENGTH('RentalContracts', 'MaintenanceDoneByTenant') IS NULL ALTER TABLE [RentalContracts] ADD [MaintenanceDoneByTenant] bit NULL;");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("IF COL_LENGTH('RentalContracts', 'MaintenanceDoneByTenant') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [MaintenanceDoneByTenant];");
    }
}
