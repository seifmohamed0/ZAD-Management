using MediatR;
using ZAD_Management.Application.Features.Settings.Companies.DTOs;

namespace ZAD_Management.Application.Features.Settings.Companies.Queries.GetAllCompanies;

public record GetAllCompaniesQuery : IRequest<List<CompanyDto>>;