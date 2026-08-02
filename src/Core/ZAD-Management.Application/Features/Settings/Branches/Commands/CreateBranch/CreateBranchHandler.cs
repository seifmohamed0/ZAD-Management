using MediatR;
using ZAD_Management.Application.Interfaces.Repositories;
using ZAD_Management.Domain.Entities;

namespace ZAD_Management.Application.Features.Settings.Branches.Commands.CreateBranch;

public class CreateBranchHandler
    : IRequestHandler<CreateBranchCommand, int>
{
    private readonly IBranchRepository _branchRepository;
    private readonly ICompanyRepository _companyRepository;

    public CreateBranchHandler(
        IBranchRepository branchRepository,
        ICompanyRepository companyRepository)
    {
        _branchRepository = branchRepository;
        _companyRepository = companyRepository;
    }

    public async Task<int> Handle(
        CreateBranchCommand request,
        CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(
            request.Branch.CompanyId,
            cancellationToken);

        if (company == null)
        {
            throw new Exception("Company not found.");
        }

        var branch = new Branch
        {
            CompanyId = request.Branch.CompanyId,
            Code = request.Branch.Code,
            ArabicName = request.Branch.ArabicName,
            EnglishName = request.Branch.EnglishName,
            ArabicAddress = request.Branch.ArabicAddress,
            EnglishAddress = request.Branch.EnglishAddress,
            Phone = request.Branch.Phone,
            Logo = request.Branch.Logo
        };

        return await _branchRepository.AddAsync(branch);
    }
}