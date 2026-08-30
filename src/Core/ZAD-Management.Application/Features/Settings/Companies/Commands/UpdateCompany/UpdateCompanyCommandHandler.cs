using MediatR;
using ZAD_Management.Application.Interfaces.Repositories;

namespace ZAD_Management.Application.Features.Settings.Companies.Commands.UpdateCompany;

public class UpdateCompanyHandler 
    : IRequestHandler<UpdateCompanyCommand, bool>
{
    private readonly ICompanyRepository _companyRepository;

    public UpdateCompanyHandler(
        ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }


    public async Task<bool> Handle(
        UpdateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(
            request.Id,
            cancellationToken);
        if (company == null)
            return false;


        company.Code = request.Company.Code ?? company.Code;
        company.ArabicName = request.Company.ArabicName ?? company.ArabicName;
        company.EnglishName = request.Company.EnglishName ?? company.EnglishName;
        company.ArabicAddress = request.Company.ArabicAddress ?? string.Empty;
        company.EnglishAddress = request.Company.EnglishAddress ?? string.Empty;
        company.Country = string.IsNullOrWhiteSpace(request.Company.Country) ? company.Country : request.Company.Country;
        company.City = string.IsNullOrWhiteSpace(request.Company.City) ? company.City : request.Company.City;
        company.Language = string.IsNullOrWhiteSpace(request.Company.Language) ? company.Language : request.Company.Language;
        company.Phone = request.Company.Phone ?? string.Empty;
        company.Website = request.Company.Website ?? string.Empty;
        company.Logo = request.Company.Logo ?? string.Empty;
        company.IsActive = request.Company.IsActive;
        company.UpdatedAt = DateTime.UtcNow;


        await _companyRepository.UpdateAsync(company);

        return true;
    }
}