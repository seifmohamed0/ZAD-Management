using ZAD_Management.Domain.Enums;

namespace ZAD_Management.Domain.ValueObjects;

public class MaintenanceAlert
{
    public DateTime? NextMaintenanceDate { get; private set; }
    public decimal? NextMaintenanceKm { get; private set; }
    public int ReminderBeforePeriodicMaintenance { get; private set; }
    public NotificationType NotificationType { get; private set; }

    private MaintenanceAlert() { }

    public MaintenanceAlert(
        DateTime? nextMaintenanceDate,
        decimal? nextMaintenanceKm,
        int reminderBeforePeriodicMaintenance = 7,
        NotificationType notificationType = NotificationType.Kilometer)
    {
        NextMaintenanceDate = nextMaintenanceDate;
        NextMaintenanceKm = nextMaintenanceKm;
        ReminderBeforePeriodicMaintenance = reminderBeforePeriodicMaintenance;
        NotificationType = notificationType;
    }
}

