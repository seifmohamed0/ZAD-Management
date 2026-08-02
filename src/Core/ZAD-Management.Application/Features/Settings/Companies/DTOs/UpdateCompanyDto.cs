namespace ZAD_Management.Application.Features.Settings.Companies.DTOs;

public class UpdateCompanyDto
{
    public string Code { get; set; } = string.Empty;

    public string ArabicName { get; set; } = string.Empty;

    public string EnglishName { get; set; } = string.Empty;

    public string ArabicAddress { get; set; } = string.Empty;

    public string EnglishAddress { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Language { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Website { get; set; } = string.Empty;

    public string Logo { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}