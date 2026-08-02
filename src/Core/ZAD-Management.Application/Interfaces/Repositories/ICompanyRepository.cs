using ZAD_Management.Domain.Entities;

namespace ZAD_Management.Application.Interfaces.Repositories;

public interface ICompanyRepository
{
    Task<List<Company>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<Company?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken);

    Task<int> AddAsync(
        Company company);

    Task UpdateAsync(
        Company company);

}