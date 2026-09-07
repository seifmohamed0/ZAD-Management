using System;

namespace ZAD_Management.Application.Features.Rentals.Contracts.DTOs;

public class CloseRentalContractDto
{
    public DateTime ActualReturnDate { get; set; }
    public decimal ReturnKm { get; set; }
}

