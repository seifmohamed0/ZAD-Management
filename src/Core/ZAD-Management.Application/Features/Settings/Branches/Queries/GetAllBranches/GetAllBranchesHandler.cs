using MediatR;
using ZAD_Management.Application.Interfaces.Repositories;
using ZAD_Management.Domain.Entities;

namespace ZAD_Management.Application.Features.Settings.Branches.Queries.GetAllBranches;

public class GetAllBranchesHandler
    : IRequestHandler<GetAllBranchesQuery, List<Branch>>
{
    private readonly IBranchRepository _branchRepository;

    public GetAllBranchesHandler(
        IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<List<Branch>> Handle(
        GetAllBranchesQuery request,
        CancellationToken cancellationToken)
    {
        return await _branchRepository.GetAllAsync(cancellationToken);
    }
}