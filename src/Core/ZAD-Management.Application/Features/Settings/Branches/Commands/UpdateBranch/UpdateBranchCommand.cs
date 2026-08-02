using MediatR;
using ZAD_Management.Application.Features.Settings.Branches.DTOs;

namespace ZAD_Management.Application.Features.Settings.Branches.Commands.UpdateBranch;

public record UpdateBranchCommand(
    int Id,
    UpdateBranchDto Branch
) : IRequest<bool>;