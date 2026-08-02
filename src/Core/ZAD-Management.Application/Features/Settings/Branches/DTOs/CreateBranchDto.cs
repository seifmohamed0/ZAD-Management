namespace ZAD_Management.Application.Features.Settings.Branches.DTOs;

public class CreateBranchDto
{
    public int CompanyId { get; set; }

    public string Code { get; set; } = string.Empty;

    public string ArabicName { get; set; } = string.Empty;

    public string EnglishName { get; set; } = string.Empty;

    public string ArabicAddress { get; set; } = string.Empty;

    public string EnglishAddress { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Logo { get; set; } = string.Empty;
}