using MediatR;
using ZAD_Management.Application.Features.Settings.Companies.DTOs;

namespace ZAD_Management.Application.Features.Settings.Companies.Queries.GetCompanyById;

public record GetCompanyByIdQuery(int Id)
    : IRequest<CompanyDto?>;