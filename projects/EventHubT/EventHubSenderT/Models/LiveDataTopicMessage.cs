namespace EventHubSenderT.Models;

public class LiveDataTopicMessage
{
    public Guid TenantId { get; set; }
    public Guid DeviceId { get; set; }

    public DateTimeOffset Timestamp { get; set; }

    public bool HasOeeConfig { get; set; } = false;
    public bool HasOeeAttribute { get; set; } = false;
    public CalculationMode AvailabilityMode { get; set; }
}