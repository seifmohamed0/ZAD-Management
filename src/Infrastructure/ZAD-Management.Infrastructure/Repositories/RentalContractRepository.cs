using Microsoft.EntityFrameworkCore;
using ZAD_Management.Application.Interfaces.Repositories;
using ZAD_Management.Domain.Entities;
using ZAD_Management.Infrastructure.Persistence;

namespace ZAD_Management.Infrastructure.Repositories;

public class RentalContractRepository : IRentalContractRepository
{
    private readonly ApplicationDbContext _context;

    public RentalContractRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(RentalContract contract, CancellationToken cancellationToken = default)
    {
        _context.RentalContracts.Add(contract);
        await _context.SaveChangesAsync(cancellationToken);
        return contract.Id;
    }

    public async Task<List<RentalContract>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.RentalContracts
            .Include(c => c.Company)
            .Include(c => c.Branch)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<RentalContract>> GetByBranchIdAsync(int branchId, CancellationToken cancellationToken = default)
    {
        return await _context.RentalContracts
            .Include(c => c.Company)
            .Include(c => c.Branch)
            .Where(c => c.BranchId == branchId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<RentalContract?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.RentalContracts
            .Include(c => c.Company)
            .Include(c => c.Branch)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<RentalContract?> GetByContractNumberAsync(string contractNumber, CancellationToken cancellationToken = default)
    {
        return await _context.RentalContracts
            .Include(c => c.Company)
            .Include(c => c.Branch)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ContractNumber == contractNumber, cancellationToken);
    }

    public async Task UpdateAsync(RentalContract contract, CancellationToken cancellationToken = default)
    {
        _context.RentalContracts.Update(contract);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

