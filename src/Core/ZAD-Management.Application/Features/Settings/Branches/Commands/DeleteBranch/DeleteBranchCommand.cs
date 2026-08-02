using MediatR;

namespace ZAD_Management.Application.Features.Settings.Branches.Commands.DeleteBranch;

public record DeleteBranchCommand(int Id) : IRequest<bool>;