using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZAD_Management.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRentalContracts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RentalContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    ContractNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountingNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReferenceNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ContractType = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    WithDriver = table.Column<bool>(type: "bit", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    StartDay = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ExpectedReceivingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedReceivingTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DeliveryDay = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PeriodInDays = table.Column<int>(type: "int", nullable: false),
                    ActualPeriodInDays = table.Column<int>(type: "int", nullable: false),
                    TenantName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TenantLicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenantPassportNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TenantUnifiedNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TenantIdNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenantMobile = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TenantBirthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantAge = table.Column<int>(type: "int", nullable: true),
                    SponsorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SponsorNationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SponsorLicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SponsorLicenseExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SponsorIdNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SponsorIdExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecondDriverName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SecondDriverNationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SecondDriverLicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SecondDriverLicenseExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecondDriverIdNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SecondDriverIdExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VehiclePlateNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VehicleModelYear = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    VehicleFileNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartKilometerCounter = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ReturnKilometerCounter = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    RentPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NetRentPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DelayPenaltyPerHour = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AllowedDelayHours = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    MaintenancePenalty = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AccidentPenalty = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DriverFare = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    DriverWorkingHoursPerDay = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    DriverOvertimeAmountPerHour = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    DriverDailyRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    KilometerPerDay = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MaximumKilometerPerDay = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AmountOfKmExceedingLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NextMaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextMaintenanceKm = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ReminderBeforePeriodicMaintenance = table.Column<int>(type: "int", nullable: true),
                    NotificationType = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalContracts_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentalContracts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentalContracts_BranchId",
                table: "RentalContracts",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalContracts_CompanyId",
                table: "RentalContracts",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalContracts");
        }
    }
}
