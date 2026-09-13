namespace ZAD_Management.Application.Features.Rentals.Contracts.DTOs;

public class ReceiveVehicleDto
{
    public DateTime ReceivingDate { get; set; }
    public decimal ReceivingKilometerCounter { get; set; }
    public bool MaintenanceDoneByTenant { get; set; }
    public string? Notes { get; set; }
}
