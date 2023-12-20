namespace EventHubSenderT.Models;

/// <summary>
/// Represents a message that is put on the EventHub.
/// </summary>
/// <param name="SignalTowerId">Id of the SignalTower to which this event is targeted</param>
/// <param name="Event">event data</param>
public record class OeeEventHubMessage(
    Guid SignalTowerId,
    TimeKeepingOeeEvent @Event
    )
{
    /// <summary>The EventHub PartitionKey for this message.</summary>
    public string PartitionKey => SignalTowerId.ToString("N");

    /// <summary>A unique identifier for this particular message.</summary>
    /// <remarks>Combines information about SignalTowerId and the Event's type and timestamp
    /// to create a unique id, that can be used to deduplicate the message stream.</remarks>
    public string GetMessageId()
        => $"{SignalTowerId.ForMessageId()}{(int)@Event.EventType}{@Event.UtcTimestamp.DayOfYear:000}{(int)@Event.UtcTimestamp.TimeOfDay.TotalMinutes:00000}";

    /* +---------------+-----------+--------------+-------------------------+
     * |   8 digits    |  1 digit  |    3 digits  |     5 digits            |
     * +---------------+-----------+--------------+-------------------------+
     * | SignalTowerId | EventType | TS DayInYear | TS TimeOfDay in minutes |
     * +---------------+-----------+--------------+-------------------------+
     * | GUID          |  [1..6]   |  [001..366]  |    [00000..86400]       |
     * +---------------+-----------+--------------+-------------------------+
     */

}

internal static class GuidExtensions
{
    internal static string ForMessageId(this Guid guid)
        => Convert.ToBase64String(guid.ToByteArray()[^6..]);
}