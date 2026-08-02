using MediatR;
using ZAD_Management.Application.Interfaces.Repositories;

namespace ZAD_Management.Application.Features.Settings.Companies.Commands.DeleteCompany;

public class DeleteCompanyHandler
    : IRequestHandler<DeleteCompanyCommand, bool>
{
    private readonly ICompanyRepository _companyRepository;

    public DeleteCompanyHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<bool> Handle(
        DeleteCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (company == null)
            return false;

        company.IsActive = false;
        company.UpdatedAt = DateTime.UtcNow;

        await _companyRepository.UpdateAsync(company);

        return true;
    }
}