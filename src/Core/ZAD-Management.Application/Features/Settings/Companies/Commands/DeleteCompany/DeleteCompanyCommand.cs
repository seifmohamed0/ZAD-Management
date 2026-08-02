using MediatR;

namespace ZAD_Management.Application.Features.Settings.Companies.Commands.DeleteCompany;

public record DeleteCompanyCommand(int Id)
    : IRequest<bool>;