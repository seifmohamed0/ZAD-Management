namespace ZAD_Management.Application.Features.Settings.Companies.DTOs;

public class CompanyDto
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string ArabicName { get; set; } = string.Empty;

    public string EnglishName { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}