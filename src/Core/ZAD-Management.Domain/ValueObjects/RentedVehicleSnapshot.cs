namespace ZAD_Management.Domain.ValueObjects;

public class RentedVehicleSnapshot
{
    public string PlateNo { get; private set; } = string.Empty;
    public string ModelYear { get; private set; } = string.Empty;
    public decimal KilometerCounter { get; private set; }
    private RentedVehicleSnapshot() { }

    public RentedVehicleSnapshot(
        string plateNo,
        string modelYear,
        decimal kilometerCounter)
    {
        PlateNo = plateNo;
        ModelYear = modelYear;
        KilometerCounter = kilometerCounter;
    }
}

