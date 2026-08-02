using MediatR;
using ZAD_Management.Application.Features.Settings.Companies.DTOs;
using ZAD_Management.Application.Interfaces.Repositories;

namespace ZAD_Management.Application.Features.Settings.Companies.Queries.GetCompanyById;

public class GetCompanyByIdHandler
    : IRequestHandler<GetCompanyByIdQuery, CompanyDto?>
{
    private readonly ICompanyRepository _companyRepository;

    public GetCompanyByIdHandler(
        ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<CompanyDto?> Handle(
        GetCompanyByIdQuery request,
        CancellationToken cancellationToken)
    {
        var company = await _companyRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (company == null)
            return null;

        return new CompanyDto
        {
            Id = company.Id,
            Code = company.Code,
            ArabicName = company.ArabicName,
            EnglishName = company.EnglishName,
            Country = company.Country,
            City = company.City,
            Phone = company.Phone,
            IsActive = company.IsActive
        };
    }
}