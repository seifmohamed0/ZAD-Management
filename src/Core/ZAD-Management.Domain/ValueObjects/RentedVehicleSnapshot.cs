namespace ZAD_Management.Domain.ValueObjects;

public class RentedVehicleSnapshot
{
    public string PlateNo { get; private set; } = string.Empty;
    public string ModelYear { get; private set; } = string.Empty;
    public string FileNo { get; private set; } = string.Empty;
    public decimal KilometerCounter { get; private set; }

    public decimal KilometerPerDay { get; private set; }
    public decimal MaximumKilometerPerDay { get; private set; }

    public DateTime? NextMaintenanceDate { get; private set; }
    public decimal? NextMaintenanceKm { get; private set; }

    private RentedVehicleSnapshot() { }

    public RentedVehicleSnapshot(
        string plateNo,
        string modelYear,
        decimal kilometerCounter,
        decimal kilometerPerDay = 0,
        decimal maximumKilometerPerDay = 0,
        DateTime? nextMaintenanceDate = null,
        decimal? nextMaintenanceKm = null,
        string fileNo = "")
    {
        if (string.IsNullOrWhiteSpace(plateNo))
            throw new ArgumentException("Vehicle plate number is required.", nameof(plateNo));

        if (kilometerCounter < 0)
            throw new ArgumentOutOfRangeException(nameof(kilometerCounter), "Starting kilometer counter cannot be negative.");

        if (kilometerPerDay < 0 || maximumKilometerPerDay < 0)
            throw new ArgumentOutOfRangeException(nameof(kilometerPerDay), "Vehicle mileage limits cannot be negative.");

        if (nextMaintenanceKm < 0)
            throw new ArgumentOutOfRangeException(nameof(nextMaintenanceKm), "Next maintenance kilometer cannot be negative.");

        PlateNo = plateNo;
        ModelYear = modelYear;
        FileNo = fileNo;
        KilometerCounter = kilometerCounter;

        KilometerPerDay = kilometerPerDay;
        MaximumKilometerPerDay = maximumKilometerPerDay;

        NextMaintenanceDate = nextMaintenanceDate;
        NextMaintenanceKm = nextMaintenanceKm;
    }

    public void UpdateMileagePolicy(decimal kilometerPerDay, decimal maximumKilometerPerDay)
    {
        if (kilometerPerDay < 0 || maximumKilometerPerDay < 0)
            throw new ArgumentOutOfRangeException(nameof(kilometerPerDay), "Vehicle mileage limits cannot be negative.");

        KilometerPerDay = kilometerPerDay;
        MaximumKilometerPerDay = maximumKilometerPerDay;
    }
}
