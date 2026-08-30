namespace ZAD_Management.Domain.ValueObjects;

public class PrivateDriverTerms
{
    public decimal DriverFare { get; private set; }
    public decimal DriverWorkingHoursPerDay { get; private set; }
    public decimal DriverOvertimeAmountPerHour { get; private set; }
    public decimal DailyRate { get; private set; }

    private PrivateDriverTerms() { }

    public PrivateDriverTerms(
        decimal driverFare,
        decimal driverWorkingHoursPerDay,
        decimal driverOvertimeAmountPerHour,
        decimal dailyRate)
    {
        DriverFare = Math.Max(0, driverFare);
        DriverWorkingHoursPerDay = Math.Max(0, driverWorkingHoursPerDay);
        DriverOvertimeAmountPerHour = Math.Max(0, driverOvertimeAmountPerHour);
        DailyRate = Math.Max(0, dailyRate);
    }
}

