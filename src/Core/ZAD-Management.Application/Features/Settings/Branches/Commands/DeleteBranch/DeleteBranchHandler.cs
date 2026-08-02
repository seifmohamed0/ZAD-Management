using MediatR;
using ZAD_Management.Application.Interfaces.Repositories;

namespace ZAD_Management.Application.Features.Settings.Branches.Commands.DeleteBranch;

public class DeleteBranchHandler
    : IRequestHandler<DeleteBranchCommand, bool>
{
    private readonly IBranchRepository _branchRepository;

    public DeleteBranchHandler(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<bool> Handle(
        DeleteBranchCommand request,
        CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (branch == null)
            return false;

        branch.IsActive = false;
        branch.UpdatedAt = DateTime.UtcNow;

        await _branchRepository.UpdateAsync(branch);

        return true;
    }
}