using MediatR;
using ZAD_Management.Application.Features.Settings.Companies.DTOs;
using ZAD_Management.Application.Interfaces.Repositories;

namespace ZAD_Management.Application.Features.Settings.Companies.Queries.GetAllCompanies;

public class GetAllCompaniesHandler
    : IRequestHandler<GetAllCompaniesQuery, List<CompanyDto>>
{
    private readonly ICompanyRepository _repository;

    public GetAllCompaniesHandler(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CompanyDto>> Handle(
        GetAllCompaniesQuery request,
        CancellationToken cancellationToken)
    {
        var companies = await _repository.GetAllAsync(cancellationToken);

        return companies.Select(x => new CompanyDto
        {
            Id = x.Id,
            Code = x.Code,
            ArabicName = x.ArabicName,
            EnglishName = x.EnglishName,
            Country = x.Country,
            City = x.City,
            Phone = x.Phone,
            IsActive = x.IsActive
        }).ToList();
    }
}