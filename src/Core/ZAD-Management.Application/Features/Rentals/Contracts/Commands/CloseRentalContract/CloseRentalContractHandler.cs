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

        var strategy = RentalCalculationStrategyFactory.GetStrategy(contract.ContractType);

        var calculationResult = RentalCalculationStrategyFactory.GetStrategy(contract.ContractType).Calculate(contract, request.ActualReturnDate, request.ReturnKm);

        contract.Close(request.ActualReturnDate, request.ReturnKm, calculationResult);

        await _contractRepository.UpdateAsync(contract, cancellationToken);

        return calculationResult;
    }
}

