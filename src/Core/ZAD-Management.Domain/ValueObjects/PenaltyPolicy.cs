namespace ZAD_Management.Domain.ValueObjects;

public class PenaltyPolicy
{
    public decimal DelayPenaltyPerHour { get; private set; }
    public decimal AllowedDelayHours { get; private set; }
    public decimal MaintenancePenalty { get; private set; }
    public decimal AccidentPenalty { get; private set; }
    public decimal AmountOfKmExceedingTheLimit { get; private set; }

    private PenaltyPolicy() { }

    public PenaltyPolicy(
        decimal delayPenaltyPerHour,
        decimal allowedDelayHours,
        decimal maintenancePenalty,
        decimal accidentPenalty,
        decimal amountOfKmExceedingTheLimit = 0)
    {
        if (delayPenaltyPerHour < 0)
            throw new ArgumentException(
                "Delay penalty cannot be negative.",
                nameof(delayPenaltyPerHour));

        if (allowedDelayHours < 0)
            throw new ArgumentException(
                "Allowed delay hours cannot be negative.",
                nameof(allowedDelayHours));

        if (maintenancePenalty < 0)
            throw new ArgumentException(
                "Maintenance penalty cannot be negative.",
                nameof(maintenancePenalty));

        if (accidentPenalty < 0)
            throw new ArgumentException(
                "Accident penalty cannot be negative.",
                nameof(accidentPenalty));

        if (amountOfKmExceedingTheLimit < 0)
            throw new ArgumentException(
                "The excess kilometer amount cannot be negative.",
                nameof(amountOfKmExceedingTheLimit));

        DelayPenaltyPerHour = delayPenaltyPerHour;
        AllowedDelayHours = allowedDelayHours;
        MaintenancePenalty = maintenancePenalty;
        AccidentPenalty = accidentPenalty;
        AmountOfKmExceedingTheLimit = amountOfKmExceedingTheLimit;
    }
}