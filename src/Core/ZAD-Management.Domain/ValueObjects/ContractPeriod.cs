namespace ZAD_Management.Domain.ValueObjects;

public class ContractPeriod
{
    public DateTime StartDate { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public string StartDay { get; private set; } = string.Empty;
    public DateTime ExpectedReceivingDate { get; private set; }
    public TimeSpan ExpectedReceivingTime { get; private set; }
    public string DeliveryDay { get; private set; } = string.Empty;
    public int PeriodInDays { get; private set; }
    public int ActualPeriodInDays { get; private set; }

    // EF Core parameterless constructor
    private ContractPeriod() { }

    public ContractPeriod(
        DateTime startDate,
        TimeSpan startTime,
        DateTime expectedReceivingDate,
        TimeSpan expectedReceivingTime,
        int? periodInDays = null)
    {
        if (expectedReceivingDate.Date < startDate.Date)
            throw new ArgumentException("Expected receiving date cannot be earlier than start date.");

        StartDate = startDate.Date;
        StartTime = startTime;
        StartDay = startDate.DayOfWeek.ToString();

        ExpectedReceivingDate = expectedReceivingDate.Date;
        ExpectedReceivingTime = expectedReceivingTime;
        DeliveryDay = expectedReceivingDate.DayOfWeek.ToString();

        int calculatedDays = (expectedReceivingDate.Date - startDate.Date).Days;
        if (calculatedDays == 0) calculatedDays = 1;

        PeriodInDays = periodInDays.HasValue && periodInDays.Value > 0 ? periodInDays.Value : calculatedDays;
        ActualPeriodInDays = 0;
    }

    public void RecordActualReturn(DateTime actualReturnDate)
    {
        ActualPeriodInDays = Math.Max(1, (actualReturnDate.Date - StartDate.Date).Days);
    }
}

