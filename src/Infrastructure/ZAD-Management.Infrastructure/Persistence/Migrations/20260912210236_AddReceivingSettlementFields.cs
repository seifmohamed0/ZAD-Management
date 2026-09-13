using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZAD_Management.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddReceivingSettlementFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountOfKmExceedingTheLimit",
                table: "RentalContracts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExitDiscountAmount",
                table: "RentalContracts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaintenancePaidByTenant",
                table: "RentalContracts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MaintenanceDoneByTenant",
                table: "RentalContracts",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfKmExceedingTheLimit",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "ExitDiscountAmount",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "MaintenancePaidByTenant",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "MaintenanceDoneByTenant",
                table: "RentalContracts");
        }
    }
}
