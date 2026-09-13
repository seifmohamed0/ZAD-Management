using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZAD_Management.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRentalReceivingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ReceivingPeriodInDays",
                table: "RentalContracts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ReceivingTotalConsumptionKilometers",
                table: "RentalContracts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualReceivingDate",
                table: "RentalContracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ReceivingDelayHours",
                table: "RentalContracts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ReceivingExceededKilometers",
                table: "RentalContracts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ReceivingExceededKilometersAmount",
                table: "RentalContracts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ReceivingFreeKilometers",
                table: "RentalContracts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ReceivingKilometerCounter",
                table: "RentalContracts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceivingNotes",
                table: "RentalContracts",
                type: "nvarchar(max)",
                nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualReceivingDate",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "ReceivingDelayHours",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "ReceivingExceededKilometers",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "ReceivingExceededKilometersAmount",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "ReceivingFreeKilometers",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "ReceivingKilometerCounter",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "ReceivingNotes",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "ReceivingTotalConsumptionKilometers",
                table: "RentalContracts");

            migrationBuilder.DropColumn(
                name: "ReceivingPeriodInDays",
                table: "RentalContracts");
        }
    }
}
