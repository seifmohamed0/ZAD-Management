using MediatR;
using ZAD_Management.Domain.Entities;

namespace ZAD_Management.Application.Features.Settings.Branches.Queries.GetBranchById;

public record GetBranchByIdQuery(int Id)
    : IRequest<Branch?>;