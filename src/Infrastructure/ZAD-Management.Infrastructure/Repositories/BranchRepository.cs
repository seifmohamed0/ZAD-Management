using Microsoft.EntityFrameworkCore;
using ZAD_Management.Application.Interfaces.Repositories;
using ZAD_Management.Domain.Entities;
using ZAD_Management.Infrastructure.Persistence;

namespace ZAD_Management.Infrastructure.Repositories;

public class BranchRepository : IBranchRepository
{
    private readonly ApplicationDbContext _context;

    public BranchRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Branch branch)
    {
        _context.Branches.Add(branch);

        await _context.SaveChangesAsync();

        return branch.Id;
    }

    public async Task<List<Branch>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Branches
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Branch?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Branches
            .AsNoTracking()
            .FirstOrDefaultAsync(
                b => b.Id == id,
                cancellationToken);
    }

    public async Task UpdateAsync(Branch branch)
    {
        _context.Branches.Update(branch);

        await _context.SaveChangesAsync();
    }
}