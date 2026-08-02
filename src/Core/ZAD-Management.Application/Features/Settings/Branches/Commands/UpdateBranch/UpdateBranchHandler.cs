using MediatR;
using ZAD_Management.Application.Interfaces.Repositories;

namespace ZAD_Management.Application.Features.Settings.Branches.Commands.UpdateBranch;

public class UpdateBranchHandler
    : IRequestHandler<UpdateBranchCommand, bool>
{
    private readonly IBranchRepository _branchRepository;
    private readonly ICompanyRepository _companyRepository;

    public UpdateBranchHandler(
        IBranchRepository branchRepository,
        ICompanyRepository companyRepository)
    {
        _branchRepository = branchRepository;
        _companyRepository = companyRepository;
    }

    public async Task<bool> Handle(
        UpdateBranchCommand request,
        CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (branch == null)
            return false;

        var company = await _companyRepository.GetByIdAsync(
            request.Branch.CompanyId,
            cancellationToken);

        if (company == null)
            return false;

        branch.CompanyId = request.Branch.CompanyId;
        branch.Code = request.Branch.Code;
        branch.ArabicName = request.Branch.ArabicName;
        branch.EnglishName = request.Branch.EnglishName;
        branch.ArabicAddress = request.Branch.ArabicAddress;
        branch.EnglishAddress = request.Branch.EnglishAddress;
        branch.Phone = request.Branch.Phone;
        branch.Logo = request.Branch.Logo;
        branch.IsActive = request.Branch.IsActive;
        branch.UpdatedAt = DateTime.UtcNow;

        await _branchRepository.UpdateAsync(branch);

        return true;
    }
}