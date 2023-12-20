using System.Text.Json.Serialization;

namespace EventHubSenderT.Models;

public readonly record struct TimeKeepingOeeEvent(
    [property: JsonPropertyName("ts")] DateTimeOffset UtcTimestamp,
    [property: JsonPropertyName("et")] TimeKeepingOeeEventType EventType,
    [property: JsonPropertyName("id")] Guid RelatedId,
    [property: JsonPropertyName("cm")] CalculationMode CalculationMode,
    [property: JsonPropertyName("dm")] CalculationMode? DeviationMode)
{
    public static TimeKeepingOeeEvent StartShift(DateTimeOffset timestamp, Guid shiftId, CalculationMode mode)
        => new(timestamp, TimeKeepingOeeEventType.StartShift, shiftId, mode, null);

    public static TimeKeepingOeeEvent PauseShift(DateTimeOffset timestamp, Guid shiftId, CalculationMode mode)
        => new(timestamp, TimeKeepingOeeEventType.PauseShift, shiftId, mode, null);

    public static TimeKeepingOeeEvent ResumeShift(DateTimeOffset timestamp, Guid shiftId, CalculationMode mode)
        => new(timestamp, TimeKeepingOeeEventType.ResumeShift, shiftId, mode, null);

    public static TimeKeepingOeeEvent EndShift(DateTimeOffset timestamp, Guid shiftId, CalculationMode mode)
        => new(timestamp, TimeKeepingOeeEventType.EndShift, shiftId, mode, null);

    public static TimeKeepingOeeEvent StartDeviation(DateTimeOffset timestamp, Guid deviationId, CalculationMode oeeMode, CalculationMode deviationMode)
        => new(timestamp, TimeKeepingOeeEventType.StartDeviation, deviationId, oeeMode, deviationMode);

    public static TimeKeepingOeeEvent EndDeviation(DateTimeOffset timestamp, Guid deviationId, CalculationMode oeeMode, CalculationMode deviationMode)
        => new(timestamp, TimeKeepingOeeEventType.EndDeviation, deviationId, oeeMode, deviationMode);
}

public enum TimeKeepingOeeEventType
{
    Undefined = 0,
    StartShift,
    PauseShift,
    ResumeShift,
    EndShift,
    StartDeviation,
    EndDeviation
}

public enum CalculationMode
{
    /// <summary>
    /// "Tatsächlich"
    /// </summary>
    Additive,
    /// <summary>
    /// "Abzüglich" 
    /// </summary>
    Subtractive
}