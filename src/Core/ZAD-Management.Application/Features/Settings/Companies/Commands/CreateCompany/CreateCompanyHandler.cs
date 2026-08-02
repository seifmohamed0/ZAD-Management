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
            Code = request.Company.Code,
            ArabicName = request.Company.ArabicName,
            EnglishName = request.Company.EnglishName,
            ArabicAddress = request.Company.ArabicAddress,
            EnglishAddress = request.Company.EnglishAddress,
            Country = request.Company.Country,
            City = request.Company.City,
            Language = request.Company.Language,
            Phone = request.Company.Phone,
            Website = request.Company.Website,
            Logo = request.Company.Logo
        };

        return await _companyRepository.AddAsync(company);
    }
}