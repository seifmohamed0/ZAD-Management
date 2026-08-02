using ZAD_Management.Domain.Entities;

namespace ZAD_Management.Application.Interfaces.Repositories;

public interface IBranchRepository
{
    Task<List<Branch>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Branch?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<int> AddAsync(
        Branch branch);

    Task UpdateAsync(
        Branch branch);
}