using MediatR;
using ZAD_Management.Application.Features.Settings.Branches.DTOs;

namespace ZAD_Management.Application.Features.Settings.Branches.Commands.CreateBranch;

public record CreateBranchCommand(
    CreateBranchDto Branch
) : IRequest<int>;