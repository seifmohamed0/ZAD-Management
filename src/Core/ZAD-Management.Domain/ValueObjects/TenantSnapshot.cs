namespace ZAD_Management.Domain.ValueObjects;

public class TenantSnapshot
{
    public string TenantName { get; private set; } = string.Empty;
    public string LicenseNumber { get; private set; } = string.Empty;
    public string IdNumber { get; private set; } = string.Empty;
    public string Mobile { get; private set; } = string.Empty;

    private TenantSnapshot() { }

    public TenantSnapshot(
        string tenantName,
        string licenseNumber,
        string idNumber,
        string mobile
    )
    {
        TenantName = tenantName;
        LicenseNumber = licenseNumber;
        IdNumber = idNumber;
        Mobile = mobile;
    }
}

