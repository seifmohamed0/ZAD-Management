namespace ZAD_Management.Domain.ValueObjects;

public class DriverSnapshot
{
    public string? SecondDriverName { get; private set; }
    public string? Nationality { get; private set; }
    public string? LicenseNumber { get; private set; }
    public DateTime? LicenseExpireDate { get; private set; }
    public string? IdNumber { get; private set; }
    public DateTime? IdExpireDate { get; private set; }

    private DriverSnapshot() { }

    public DriverSnapshot(
        string? secondDriverName,
        string? nationality,
        string? licenseNumber,
        DateTime? licenseExpireDate,
        string? idNumber,
        DateTime? idExpireDate)
    {
        SecondDriverName = secondDriverName;
        Nationality = nationality;
        LicenseNumber = licenseNumber;
        LicenseExpireDate = licenseExpireDate;
        IdNumber = idNumber;
        IdExpireDate = idExpireDate;
    }
}

