using MediatR;
using ZAD_Management.Application.Interfaces.Repositories;
using ZAD_Management.Domain.Entities;

namespace ZAD_Management.Application.Features.Settings.Companies.Commands.CreateCompany;

public class CreateCompanyHandler
    : IRequestHandler<CreateCompanyCommand, int>
{
    private readonly ICompanyRepository _companyRepository;

    public CreateCompanyHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<int> Handle(
        CreateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var company = new Company
        {
            Code = request.Company.Code ?? string.Empty,
            ArabicName = request.Company.ArabicName ?? string.Empty,
            EnglishName = request.Company.EnglishName ?? string.Empty,
            ArabicAddress = request.Company.ArabicAddress ?? string.Empty,
            EnglishAddress = request.Company.EnglishAddress ?? string.Empty,
            Country = string.IsNullOrWhiteSpace(request.Company.Country) ? "Saudi Arabia" : request.Company.Country,
            City = string.IsNullOrWhiteSpace(request.Company.City) ? "Riyadh" : request.Company.City,
            Language = string.IsNullOrWhiteSpace(request.Company.Language) ? "ar" : request.Company.Language,
            Phone = request.Company.Phone ?? string.Empty,
            Website = request.Company.Website ?? string.Empty,
            Logo = request.Company.Logo ?? string.Empty
        };

        return await _companyRepository.AddAsync(company);
    }
}