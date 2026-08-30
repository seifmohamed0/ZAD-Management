namespace ZAD_Management.Domain.ValueObjects;

public class SponsorSnapshot
{
    public string? SponsorName { get; private set; }
    public string? Nationality { get; private set; }
    public string? LicenseNumber { get; private set; }
    public DateTime? LicenseExpireDate { get; private set; }
    public string? IdNumber { get; private set; }
    public DateTime? IdExpireDate { get; private set; }

    private SponsorSnapshot() { }

    public SponsorSnapshot(
        string? sponsorName,
        string? nationality,
        string? licenseNumber,
        DateTime? licenseExpireDate,
        string? idNumber,
        DateTime? idExpireDate)
    {
        SponsorName = sponsorName;
        Nationality = nationality;
        LicenseNumber = licenseNumber;
        LicenseExpireDate = licenseExpireDate;
        IdNumber = idNumber;
        IdExpireDate = idExpireDate;
    }
}

