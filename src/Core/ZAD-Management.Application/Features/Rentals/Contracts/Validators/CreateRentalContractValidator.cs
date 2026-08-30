using FluentValidation;
using ZAD_Management.Application.Features.Rentals.Contracts.DTOs;

namespace ZAD_Management.Application.Features.Rentals.Contracts.Validators;

public class CreateRentalContractValidator : AbstractValidator<CreateRentalContractDto>
{
    public CreateRentalContractValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("CompanyId is required.");

        RuleFor(x => x.BranchId)
            .GreaterThan(0).WithMessage("BranchId is required.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.ExpectedReceivingDate)
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("Expected receiving date must be on or after start date.");

        RuleFor(x => x.Tenant.TenantName)
            .NotEmpty().WithMessage("Tenant name is required.");

        RuleFor(x => x.Tenant.LicenseNumber)
            .NotEmpty().WithMessage("Tenant license number is required.");

        RuleFor(x => x.Tenant.IdNumber)
            .NotEmpty().WithMessage("Tenant ID number is required.");

        RuleFor(x => x.Tenant.Mobile)
            .NotEmpty().WithMessage("Tenant mobile number is required.");

        RuleFor(x => x.Vehicle.PlateNo)
            .NotEmpty().WithMessage("Vehicle plate number is required.");

        RuleFor(x => x.Pricing.RentPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Rent price cannot be negative.");
    }
}

