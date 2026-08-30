namespace ZAD_Management.Domain.ValueObjects;

public class RentedVehicleSnapshot
{
    public string PlateNo { get; private set; } = string.Empty;
    public string ModelYear { get; private set; } = string.Empty;
    public string FileNo { get; private set; } = string.Empty;
    public decimal StartKilometerCounter { get; private set; }
    public decimal? ReturnKilometerCounter { get; private set; }

    private RentedVehicleSnapshot() { }

    public RentedVehicleSnapshot(
        string plateNo,
        string modelYear,
        string fileNo,
        decimal startKilometerCounter)
    {
        PlateNo = plateNo;
        ModelYear = modelYear;
        FileNo = fileNo;
        StartKilometerCounter = startKilometerCounter;
    }

    public void RecordReturnKilometer(decimal returnKm)
    {
        if (returnKm < StartKilometerCounter)
            throw new ArgumentException("Return kilometer counter cannot be less than start kilometer counter.");
        ReturnKilometerCounter = returnKm;
    }
}

