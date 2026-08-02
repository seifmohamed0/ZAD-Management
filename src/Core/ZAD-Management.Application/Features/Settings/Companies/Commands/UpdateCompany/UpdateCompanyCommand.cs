using MediatR;
using ZAD_Management.Application.Features.Settings.Companies.DTOs;

namespace ZAD_Management.Application.Features.Settings.Companies.Commands.UpdateCompany;

public record UpdateCompanyCommand(
    int Id,
    UpdateCompanyDto Company
) : IRequest<bool>;