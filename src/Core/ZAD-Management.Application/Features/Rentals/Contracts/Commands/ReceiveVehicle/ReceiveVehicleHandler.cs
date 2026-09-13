using MediatR;
using ZAD_Management.Application.Interfaces.Repositories;
using ZAD_Management.Domain.Services.Calculations;

namespace ZAD_Management.Application.Features.Rentals.Contracts.Commands.ReceiveVehicle;

public class ReceiveVehicleHandler : IRequestHandler<ReceiveVehicleCommand, RentalReceivingResult>
{
    private readonly IRentalContractRepository _contractRepository;

    public ReceiveVehicleHandler(IRentalContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }

    public async Task<RentalReceivingResult> Handle(
        ReceiveVehicleCommand request,
        CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetByIdAsync(request.ContractId, cancellationToken);
        if (contract is null)
            throw new ArgumentException($"Contract with ID {request.ContractId} was not found.");

        if (request.ReceivingDate == default)
            throw new ArgumentException("Receiving date is required.", nameof(request.ReceivingDate));

        if (request.ReceivingKilometerCounter < 0)
            throw new ArgumentException("Receiving kilometer counter cannot be negative.", nameof(request.ReceivingKilometerCounter));

        var result = RentalReceivingCalculator.Calculate(
            contract,
            request.ReceivingDate,
            request.ReceivingKilometerCounter);

        contract.ReceiveVehicle(
            request.ReceivingDate,
            request.ReceivingKilometerCounter,
            result,
            request.MaintenanceDoneByTenant,
            request.Notes);

        await _contractRepository.UpdateAsync(contract, cancellationToken);
        return result;
    }
}
