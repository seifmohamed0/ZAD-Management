namespace ZAD_Management.Domain.ValueObjects;

public class PenaltyPolicy
{
    public decimal DelayPenaltyPerHour { get; private set; }
    public decimal AllowedDelayHours { get; private set; }
    public decimal MaintenancePenalty { get; private set; }
    public decimal AccidentPenalty { get; private set; }

    private PenaltyPolicy() { }

    public PenaltyPolicy(
        decimal delayPenaltyPerHour,
        decimal allowedDelayHours,
        decimal maintenancePenalty,
        decimal accidentPenalty)
    {
        DelayPenaltyPerHour = Math.Max(0, delayPenaltyPerHour);
        AllowedDelayHours = Math.Max(0, allowedDelayHours);
        MaintenancePenalty = Math.Max(0, maintenancePenalty);
        AccidentPenalty = Math.Max(0, accidentPenalty);
    }
}

