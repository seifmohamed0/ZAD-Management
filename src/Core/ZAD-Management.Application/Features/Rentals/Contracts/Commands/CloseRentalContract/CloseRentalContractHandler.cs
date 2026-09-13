using MediatR;
using ZAD_Management.Application.Interfaces.Repositories;
using ZAD_Management.Domain.Services.Calculations;

namespace ZAD_Management.Application.Features.Rentals.Contracts.Commands.CloseRentalContract;

public class CloseRentalContractHandler : IRequestHandler<CloseRentalContractCommand, RentalCalculationResult>
{
    private readonly IRentalContractRepository _contractRepository;

    public CloseRentalContractHandler(IRentalContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }

    public async Task<RentalCalculationResult> Handle(
        CloseRentalContractCommand request,
        CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetByIdAsync(request.ContractId, cancellationToken);
        if (contract == null)
            throw new ArgumentException($"Contract with ID {request.ContractId} was not found.");

        if (request.ReturnKm < contract.Vehicle.KilometerCounter)
            throw new ArgumentException("Return kilometer counter cannot be less than the starting counter.");

        if (request.MaintenancePenaltyAmount < 0 || request.AccidentPenaltyAmount < 0 ||
            request.DriverAmount < 0 || request.PaidAmount < 0 ||
            request.ExitDiscountAmount < 0 || request.MaintenancePaidByTenant < 0)
            throw new ArgumentException("Closing amounts cannot be negative.");

        var strategy = RentalCalculationStrategyFactory.GetStrategy(contract.ContractType);

        var calculated = strategy.Calculate(contract, request.ActualReturnDate, request.ReturnKm);
        var totalConsumption = request.ReturnKm - contract.Vehicle.KilometerCounter;
        var freeKilometers = Math.Max(0, contract.Vehicle.MaximumKilometerPerDay * calculated.ActualPeriodInDays);
        var exceededKilometers = Math.Max(0, totalConsumption - freeKilometers);
        var exceededAmount = exceededKilometers * contract.Penalties.AmountOfKmExceedingTheLimit;
        var maintenancePenaltyAmount = request.MaintenanceDoneByTenant
            ? 0
            : request.MaintenancePenaltyAmount;
        var totalAmount = calculated.TotalAmount + maintenancePenaltyAmount +
            request.AccidentPenaltyAmount + request.DriverAmount + exceededAmount -
            request.MaintenancePaidByTenant;
        var actualPeriodInDays = Math.Max(1, (decimal)Math.Ceiling((request.ActualReturnDate - contract.Period.StartDate.Add(contract.Period.StartTime)).TotalDays));
        var calculationResult = calculated with
        {
            ActualPeriodInDays = actualPeriodInDays,
            TotalConsumptionKilometers = totalConsumption,
            FreeKilometers = freeKilometers,
            ExceededKilometers = exceededKilometers,
            ExceededKilometersAmount = exceededAmount,
            MaintenancePenaltyAmount = maintenancePenaltyAmount,
            AccidentPenaltyAmount = request.AccidentPenaltyAmount,
            DriverAmount = request.DriverAmount,
            PaidAmount = request.PaidAmount,
            ExitDiscountAmount = request.ExitDiscountAmount,
            MaintenancePaidByTenant = request.MaintenancePaidByTenant,
            MaintenanceDoneByTenant = request.MaintenanceDoneByTenant,
            TotalAmount = totalAmount,
            NetDueAmount = Math.Max(0, totalAmount - request.PaidAmount - request.ExitDiscountAmount),
            DelayHours = RentalPenaltyCalculator.GetBillableDelayHours(
                contract.Period.ExpectedReceivingDate.Add(contract.Period.ExpectedReceivingTime),
                request.ActualReturnDate,
                contract.Penalties)
        };

        contract.Close(request.ActualReturnDate, request.ReturnKm, calculationResult);

        await _contractRepository.UpdateAsync(contract, cancellationToken);

        return calculationResult;
    }
}

