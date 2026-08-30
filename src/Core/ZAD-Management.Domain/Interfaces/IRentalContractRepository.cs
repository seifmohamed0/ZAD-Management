using ZAD_Management.Domain.Entities;

namespace ZAD_Management.Application.Interfaces.Repositories;

public interface IRentalContractRepository
{
    Task<List<RentalContract>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<RentalContract>> GetByBranchIdAsync(int branchId, CancellationToken cancellationToken = default);
    Task<RentalContract?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<RentalContract?> GetByContractNumberAsync(string contractNumber, CancellationToken cancellationToken = default);
    Task<int> AddAsync(RentalContract contract, CancellationToken cancellationToken = default);
    Task UpdateAsync(RentalContract contract, CancellationToken cancellationToken = default);
}

