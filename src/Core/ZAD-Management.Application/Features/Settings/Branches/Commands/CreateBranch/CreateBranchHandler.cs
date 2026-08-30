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
            Code = request.Branch.Code ?? string.Empty,
            ArabicName = request.Branch.ArabicName ?? string.Empty,
            EnglishName = request.Branch.EnglishName ?? string.Empty,
            ArabicAddress = request.Branch.ArabicAddress ?? string.Empty,
            EnglishAddress = request.Branch.EnglishAddress ?? string.Empty,
            Phone = request.Branch.Phone ?? string.Empty,
            Logo = request.Branch.Logo ?? string.Empty
        };

        return await _branchRepository.AddAsync(branch);
    }
}