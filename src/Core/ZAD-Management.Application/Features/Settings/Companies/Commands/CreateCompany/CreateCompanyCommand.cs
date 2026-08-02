using MediatR;
using ZAD_Management.Application.Features.Settings.Companies.DTOs;

namespace ZAD_Management.Application.Features.Settings.Companies.Commands.CreateCompany;

public record CreateCompanyCommand(CreateCompanyDto Company)
    : IRequest<int>;