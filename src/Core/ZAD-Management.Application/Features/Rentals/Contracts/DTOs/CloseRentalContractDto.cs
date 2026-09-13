using System;

namespace ZAD_Management.Application.Features.Rentals.Contracts.DTOs;

public class CloseRentalContractDto
{
    public DateTime ActualReturnDate { get; set; }
    public decimal ReturnKm { get; set; }
    public decimal MaintenancePenaltyAmount { get; set; }
    public decimal AccidentPenaltyAmount { get; set; }
    public decimal DriverAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal ExitDiscountAmount { get; set; }
    public decimal MaintenancePaidByTenant { get; set; }
    public bool MaintenanceDoneByTenant { get; set; }
    public string? Notes { get; set; }
}

