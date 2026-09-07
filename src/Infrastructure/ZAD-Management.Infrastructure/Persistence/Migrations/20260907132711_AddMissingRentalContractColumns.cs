using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZAD_Management.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingRentalContractColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountingNo",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "ActualPeriodInDays",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "DeliveryDay",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "DriverDailyRate",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "DriverFare",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "DriverOvertimeAmountPerHour",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "DriverWorkingHoursPerDay",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "NextMaintenanceDate",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "NextMaintenanceKm",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "NotificationType",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "ReferenceNo",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "ReminderBeforePeriodicMaintenance",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "SecondDriverIdExpireDate",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "SecondDriverIdNumber",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "SecondDriverLicenseExpireDate",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "SecondDriverLicenseNumber",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "SecondDriverName",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "SecondDriverNationality",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "SponsorName",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "StartDay",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "TenantAge",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "TenantPassportNumber",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "TenantUnifiedNumber",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "VehicleFileNo",
                table: "RentalContracts");

            migrationBuilder.RenameColumn(
                name: "SponsorNationality",
                table: "RentalContracts",
                newName: "DriverNationality");

            migrationBuilder.RenameColumn(
                name: "SponsorLicenseNumber",
                table: "RentalContracts",
                newName: "DriverLicenseNumber");

            migrationBuilder.RenameColumn(
                name: "SponsorLicenseExpireDate",
                table: "RentalContracts",
                newName: "DriverLicenseExpireDate");

            migrationBuilder.RenameColumn(
                name: "SponsorIdNumber",
                table: "RentalContracts",
                newName: "DriverIdNumber");

            migrationBuilder.RenameColumn(
                name: "SponsorIdExpireDate",
                table: "RentalContracts",
                newName: "DriverIdExpireDate");

            migrationBuilder.RenameColumn(
                name: "TenantBirthday",
                table: "RentalContracts",
                newName: "ActualReturnDate");

            migrationBuilder.RenameColumn(
                name: "ReturnKilometerCounter",
                table: "RentalContracts",
                newName: "ActualReturnKm");

            migrationBuilder.RenameColumn(
                name: "MaximumKilometerPerDay",
                table: "RentalContracts",
                newName: "FinalTotalAmount");

            migrationBuilder.RenameColumn(
                name: "KilometerPerDay",
                table: "RentalContracts",
                newName: "FinalDiscount");

            migrationBuilder.RenameColumn(
                name: "AmountOfKmExceedingLimit",
                table: "RentalContracts",
                newName: "FinalDelayPenalty");

            migrationBuilder.AlterColumn<string>(
                name: "DriverName",
                table: "RentalContracts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalBaseRent",
                table: "RentalContracts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalBaseRent",
                table: "RentalContracts");

            migrationBuilder.RenameColumn(
                name: "DriverNationality",
                table: "RentalContracts",
                newName: "SponsorNationality");

            migrationBuilder.RenameColumn(
                name: "DriverLicenseNumber",
                table: "RentalContracts",
                newName: "SponsorLicenseNumber");

            migrationBuilder.RenameColumn(
                name: "DriverLicenseExpireDate",
                table: "RentalContracts",
                newName: "SponsorLicenseExpireDate");

            migrationBuilder.RenameColumn(
                name: "DriverIdNumber",
                table: "RentalContracts",
                newName: "SponsorIdNumber");

            migrationBuilder.RenameColumn(
                name: "DriverIdExpireDate",
                table: "RentalContracts",
                newName: "SponsorIdExpireDate");

            migrationBuilder.RenameColumn(
                name: "FinalTotalAmount",
                table: "RentalContracts",
                newName: "MaximumKilometerPerDay");

            migrationBuilder.RenameColumn(
                name: "FinalDiscount",
                table: "RentalContracts",
                newName: "KilometerPerDay");

            migrationBuilder.RenameColumn(
                name: "FinalDelayPenalty",
                table: "RentalContracts",
                newName: "AmountOfKmExceedingLimit");

            migrationBuilder.RenameColumn(
                name: "ActualReturnKm",
                table: "RentalContracts",
                newName: "ReturnKilometerCounter");

            migrationBuilder.RenameColumn(
                name: "ActualReturnDate",
                table: "RentalContracts",
                newName: "TenantBirthday");

            migrationBuilder.AlterColumn<string>(
                name: "DriverName",
                table: "RentalContracts",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountingNo",
                table: "RentalContracts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActualPeriodInDays",
                table: "RentalContracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "RentalContracts",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryDay",
                table: "RentalContracts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "DriverDailyRate",
                table: "RentalContracts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DriverFare",
                table: "RentalContracts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DriverOvertimeAmountPerHour",
                table: "RentalContracts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DriverWorkingHoursPerDay",
                table: "RentalContracts",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextMaintenanceDate",
                table: "RentalContracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NextMaintenanceKm",
                table: "RentalContracts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "RentalContracts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotificationType",
                table: "RentalContracts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "RentalContracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNo",
                table: "RentalContracts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReminderBeforePeriodicMaintenance",
                table: "RentalContracts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SecondDriverIdExpireDate",
                table: "RentalContracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondDriverIdNumber",
                table: "RentalContracts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SecondDriverLicenseExpireDate",
                table: "RentalContracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondDriverLicenseNumber",
                table: "RentalContracts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondDriverName",
                table: "RentalContracts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondDriverNationality",
                table: "RentalContracts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SponsorName",
                table: "RentalContracts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartDay",
                table: "RentalContracts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TenantAge",
                table: "RentalContracts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenantPassportNumber",
                table: "RentalContracts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenantUnifiedNumber",
                table: "RentalContracts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleFileNo",
                table: "RentalContracts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
