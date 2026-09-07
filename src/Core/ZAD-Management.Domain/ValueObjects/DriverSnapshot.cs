namespace ZAD_Management.Domain.ValueObjects;

public class DriverSnapshot
{
    public string? DriverName { get; private set; }
    public string? Nationality { get; private set; }
    public string? LicenseNumber { get; private set; }
    public DateTime? LicenseExpireDate { get; private set; }
    public string? IdNumber { get; private set; }
    public DateTime? IdExpireDate { get; private set; }

    private DriverSnapshot() { }

    public DriverSnapshot(
        string? driverName,
        string? nationality,
        string? licenseNumber,
        DateTime? licenseExpireDate,
        string? idNumber,
        DateTime? idExpireDate)
    {
        DriverName = driverName;
        Nationality = nationality;
        LicenseNumber = licenseNumber;
        LicenseExpireDate = licenseExpireDate;
        IdNumber = idNumber;
        IdExpireDate = idExpireDate;
    }
}

