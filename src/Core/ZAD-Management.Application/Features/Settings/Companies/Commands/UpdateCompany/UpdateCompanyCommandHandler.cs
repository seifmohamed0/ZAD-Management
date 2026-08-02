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


        company.Code = request.Company.Code;
        company.ArabicName = request.Company.ArabicName;
        company.EnglishName = request.Company.EnglishName;
        company.ArabicAddress = request.Company.ArabicAddress;
        company.EnglishAddress = request.Company.EnglishAddress;
        company.Country = request.Company.Country;
        company.City = request.Company.City;
        company.Language = request.Company.Language;
        company.Phone = request.Company.Phone;
        company.Website = request.Company.Website;
        company.Logo = request.Company.Logo;
        company.IsActive = request.Company.IsActive;
        company.UpdatedAt = DateTime.UtcNow;


        await _companyRepository.UpdateAsync(company);

        return true;
    }
}