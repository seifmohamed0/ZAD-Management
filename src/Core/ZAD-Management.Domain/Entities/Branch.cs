namespace ZAD_Management.Domain.Entities;

public class Branch
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string Code { get; set; } = string.Empty;

    public string ArabicName { get; set; } = string.Empty;

    public string EnglishName { get; set; } = string.Empty;

    public string ArabicAddress { get; set; } = string.Empty;

    public string EnglishAddress { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Logo { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public Company Company { get; set; } = null!;
}