using MediatR;
using ZAD_Management.Application.Interfaces.Repositories;
using ZAD_Management.Domain.Entities;

namespace ZAD_Management.Application.Features.Settings.Branches.Queries.GetBranchById;

public class GetBranchByIdHandler
    : IRequestHandler<GetBranchByIdQuery, Branch?>
{
    private readonly IBranchRepository _branchRepository;

    public GetBranchByIdHandler(
        IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<Branch?> Handle(
        GetBranchByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _branchRepository.GetByIdAsync(
            request.Id,
            cancellationToken);
    }
}