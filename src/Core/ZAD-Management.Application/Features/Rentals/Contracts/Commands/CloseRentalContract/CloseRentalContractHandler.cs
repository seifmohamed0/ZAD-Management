using MediatR;
using ZAD_Management.Application.Interfaces.Repositories;
using ZAD_Management.Domain.Services.Calculations;
using ZAD_Management.Domain.ValueObjects;

namespace ZAD_Management.Application.Features.Rentals.Contracts.Commands.CloseRentalContract;

public class CloseRentalContractHandler : IRequestHandler<CloseRentalContractCommand, RentalCalculationResult>
{
    private readonly IRentalContractRepository _contractRepository;
    private readonly RentalSettlementCalculator _settlementCalculator;

    public CloseRentalContractHandler(
        IRentalContractRepository contractRepository,
        RentalSettlementCalculator settlementCalculator)
    {
        _contractRepository = contractRepository;
        _settlementCalculator = settlementCalculator;
    }

    public async Task<RentalCalculationResult> Handle(
        CloseRentalContractCommand request,
        CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetByIdAsync(request.ContractId, cancellationToken);
        if (contract == null)
            throw new ArgumentException($"Contract with ID {request.ContractId} was not found.");

        if (request.Pricing is not null)
        {
            contract.UpdatePricing(new RentalPricing(
                request.Pricing.RentPrice,
                request.Pricing.DiscountPercent,
                request.Pricing.DiscountAmount));
        }

        if (request.Mileage is not null)
        {
            contract.UpdateMileagePolicy(
                request.Mileage.KilometerPerDay,
                request.Mileage.MaximumKilometerPerDay);
        }

        if (request.Penalties is not null || request.Mileage is not null)
        {
            var penalties = request.Penalties;
            contract.UpdatePenaltyPolicy(new PenaltyPolicy(
                penalties?.DelayPenaltyPerHour ?? contract.Penalties.DelayPenaltyPerHour,
                penalties?.AllowedDelayHours ?? contract.Penalties.AllowedDelayHours,
                penalties?.MaintenancePenalty ?? contract.Penalties.MaintenancePenalty,
                penalties?.AccidentPenalty ?? contract.Penalties.AccidentPenalty,
                request.Mileage?.AmountOfKmExceedingLimit ?? contract.Penalties.AmountOfKmExceedingTheLimit));
        }

        var calculationResult = _settlementCalculator.Calculate(
            contract,
            new RentalSettlementInput(
                request.ActualReturnDate,
                request.ReturnKm,
                request.MaintenancePenaltyAmount,
                request.AccidentPenaltyAmount,
                request.DriverAmount,
                request.PaidAmount,
                request.ExitDiscountAmount,
                request.MaintenancePaidByTenant,
                request.MaintenanceDoneByTenant));

        contract.Close(request.ActualReturnDate, request.ReturnKm, calculationResult);

        await _contractRepository.UpdateAsync(contract, cancellationToken);

        return calculationResult;
    }
}

