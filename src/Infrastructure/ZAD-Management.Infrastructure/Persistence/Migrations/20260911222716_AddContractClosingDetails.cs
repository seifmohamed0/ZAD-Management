using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZAD_Management.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddContractClosingDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF COL_LENGTH('RentalContracts', 'KilometerPerDay') IS NULL ALTER TABLE [RentalContracts] ADD [KilometerPerDay] decimal(18,2) NOT NULL CONSTRAINT [DF_RentalContracts_KilometerPerDay] DEFAULT 0;
                IF COL_LENGTH('RentalContracts', 'MaximumKilometerPerDay') IS NULL ALTER TABLE [RentalContracts] ADD [MaximumKilometerPerDay] decimal(18,2) NOT NULL CONSTRAINT [DF_RentalContracts_MaximumKilometerPerDay] DEFAULT 0;
                IF COL_LENGTH('RentalContracts', 'NextMaintenanceDate') IS NULL ALTER TABLE [RentalContracts] ADD [NextMaintenanceDate] datetime2 NULL;
                IF COL_LENGTH('RentalContracts', 'NextMaintenanceKm') IS NULL ALTER TABLE [RentalContracts] ADD [NextMaintenanceKm] decimal(18,2) NULL;
                IF COL_LENGTH('RentalContracts', 'ActualReturnDate') IS NULL ALTER TABLE [RentalContracts] ADD [ActualReturnDate] datetime2 NULL;
                IF COL_LENGTH('RentalContracts', 'ReturnKilometerCounter') IS NULL ALTER TABLE [RentalContracts] ADD [ReturnKilometerCounter] decimal(18,2) NULL;
                IF COL_LENGTH('RentalContracts', 'ActualPeriodInDays') IS NULL ALTER TABLE [RentalContracts] ADD [ActualPeriodInDays] decimal(18,2) NULL;
                IF COL_LENGTH('RentalContracts', 'DelayHours') IS NULL ALTER TABLE [RentalContracts] ADD [DelayHours] decimal(18,2) NULL;
                IF COL_LENGTH('RentalContracts', 'TotalConsumptionKilometers') IS NULL ALTER TABLE [RentalContracts] ADD [TotalConsumptionKilometers] decimal(18,2) NULL;
                IF COL_LENGTH('RentalContracts', 'FreeKilometers') IS NULL ALTER TABLE [RentalContracts] ADD [FreeKilometers] decimal(18,2) NULL;
                IF COL_LENGTH('RentalContracts', 'ExceededKilometers') IS NULL ALTER TABLE [RentalContracts] ADD [ExceededKilometers] decimal(18,2) NULL;
                IF COL_LENGTH('RentalContracts', 'ExceededKilometersAmount') IS NULL ALTER TABLE [RentalContracts] ADD [ExceededKilometersAmount] decimal(18,2) NULL;
                IF COL_LENGTH('RentalContracts', 'MaintenancePenaltyAmount') IS NULL ALTER TABLE [RentalContracts] ADD [MaintenancePenaltyAmount] decimal(18,2) NULL;
                IF COL_LENGTH('RentalContracts', 'AccidentPenaltyAmount') IS NULL ALTER TABLE [RentalContracts] ADD [AccidentPenaltyAmount] decimal(18,2) NULL;
                IF COL_LENGTH('RentalContracts', 'DriverAmount') IS NULL ALTER TABLE [RentalContracts] ADD [DriverAmount] decimal(18,2) NULL;
                IF COL_LENGTH('RentalContracts', 'PaidAmount') IS NULL ALTER TABLE [RentalContracts] ADD [PaidAmount] decimal(18,2) NULL;
                IF COL_LENGTH('RentalContracts', 'TotalAmount') IS NULL ALTER TABLE [RentalContracts] ADD [TotalAmount] decimal(18,2) NULL;
                IF COL_LENGTH('RentalContracts', 'NetDueAmount') IS NULL ALTER TABLE [RentalContracts] ADD [NetDueAmount] decimal(18,2) NULL;
                IF COL_LENGTH('RentalContracts', 'ClosingNotes') IS NULL ALTER TABLE [RentalContracts] ADD [ClosingNotes] nvarchar(2000) NULL;
                """);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF COL_LENGTH('RentalContracts', 'ClosingNotes') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [ClosingNotes];
                IF COL_LENGTH('RentalContracts', 'NetDueAmount') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [NetDueAmount];
                IF COL_LENGTH('RentalContracts', 'TotalAmount') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [TotalAmount];
                IF COL_LENGTH('RentalContracts', 'PaidAmount') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [PaidAmount];
                IF COL_LENGTH('RentalContracts', 'DriverAmount') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [DriverAmount];
                IF COL_LENGTH('RentalContracts', 'AccidentPenaltyAmount') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [AccidentPenaltyAmount];
                IF COL_LENGTH('RentalContracts', 'MaintenancePenaltyAmount') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [MaintenancePenaltyAmount];
                IF COL_LENGTH('RentalContracts', 'ExceededKilometersAmount') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [ExceededKilometersAmount];
                IF COL_LENGTH('RentalContracts', 'ExceededKilometers') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [ExceededKilometers];
                IF COL_LENGTH('RentalContracts', 'FreeKilometers') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [FreeKilometers];
                IF COL_LENGTH('RentalContracts', 'TotalConsumptionKilometers') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [TotalConsumptionKilometers];
                IF COL_LENGTH('RentalContracts', 'DelayHours') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [DelayHours];
                IF COL_LENGTH('RentalContracts', 'ActualPeriodInDays') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [ActualPeriodInDays];
                IF COL_LENGTH('RentalContracts', 'ReturnKilometerCounter') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [ReturnKilometerCounter];
                IF COL_LENGTH('RentalContracts', 'ActualReturnDate') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [ActualReturnDate];
                IF COL_LENGTH('RentalContracts', 'NextMaintenanceKm') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [NextMaintenanceKm];
                IF COL_LENGTH('RentalContracts', 'NextMaintenanceDate') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [NextMaintenanceDate];
                IF COL_LENGTH('RentalContracts', 'MaximumKilometerPerDay') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [MaximumKilometerPerDay];
                IF COL_LENGTH('RentalContracts', 'KilometerPerDay') IS NOT NULL ALTER TABLE [RentalContracts] DROP COLUMN [KilometerPerDay];
                """);
        }
    }
}
