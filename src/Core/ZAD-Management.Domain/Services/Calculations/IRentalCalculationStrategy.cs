using System;
using ZAD_Management.Domain.Entities;

namespace ZAD_Management.Domain.Services.Calculations;

public interface IRentalCalculationStrategy
{
    RentalCalculationResult Calculate(RentalContract contract, DateTime actualReturnDate, decimal returnKm);
}
