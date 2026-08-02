using MediatR;
using ZAD_Management.Domain.Entities;

namespace ZAD_Management.Application.Features.Settings.Branches.Queries.GetAllBranches;

public record GetAllBranchesQuery()
    : IRequest<List<Branch>>;