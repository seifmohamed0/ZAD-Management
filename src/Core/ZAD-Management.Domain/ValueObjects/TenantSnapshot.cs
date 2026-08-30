namespace ZAD_Management.Domain.ValueObjects;

public class TenantSnapshot
{
    public string TenantName { get; private set; } = string.Empty;
    public string LicenseNumber { get; private set; } = string.Empty;
    public string? PassportNumber { get; private set; }
    public string? UnifiedNumber { get; private set; }
    public string IdNumber { get; private set; } = string.Empty;
    public string Mobile { get; private set; } = string.Empty;
    public DateTime? TenantBirthday { get; private set; }
    public int? Age { get; private set; }

    private TenantSnapshot() { }

    public TenantSnapshot(
        string tenantName,
        string licenseNumber,
        string idNumber,
        string mobile,
        string? passportNumber = null,
        string? unifiedNumber = null,
        DateTime? tenantBirthday = null)
    {
        TenantName = tenantName;
        LicenseNumber = licenseNumber;
        IdNumber = idNumber;
        Mobile = mobile;
        PassportNumber = passportNumber;
        UnifiedNumber = unifiedNumber;
        TenantBirthday = tenantBirthday;

        if (tenantBirthday.HasValue)
        {
            var today = DateTime.UtcNow.Date;
            var calculatedAge = today.Year - tenantBirthday.Value.Year;
            if (tenantBirthday.Value.Date > today.AddYears(-calculatedAge)) calculatedAge--;
            Age = calculatedAge;
        }
    }
}

