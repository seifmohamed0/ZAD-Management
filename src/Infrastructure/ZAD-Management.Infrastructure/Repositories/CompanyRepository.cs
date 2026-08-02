using Microsoft.EntityFrameworkCore;
using ZAD_Management.Application.Interfaces.Repositories;
using ZAD_Management.Domain.Entities;
using ZAD_Management.Infrastructure.Persistence;

namespace ZAD_Management.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Company company)
    {
        _context.Companies.Add(company);

        await _context.SaveChangesAsync();

        return company.Id;
    }

    public async Task<List<Company>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Companies
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Company?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _context.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(
                c => c.Id == id,
                cancellationToken);
    }
    public async Task UpdateAsync(Company company)
    {
        _context.Companies.Update(company);

        await _context.SaveChangesAsync();
    }

    // soft delete the company by setting IsActive to false

}