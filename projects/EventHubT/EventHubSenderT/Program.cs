using System.Text.Json;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Azure.Messaging.ServiceBus;
using EventHubSenderT.Models;

var eventHubConnectionString = "";
var serviceBusConnectionString = "";

var signalTowerId = Guid.NewGuid();
var timestamp = DateTimeOffset.UtcNow;

var eventHubClient = new EventHubProducerClient(eventHubConnectionString);
var sender = new ServiceBusClient(serviceBusConnectionString).CreateSender("sbt-wea-livedata-test-gwc");

try
{
    await SendEventHubEventAsync(eventHubClient, signalTowerId, timestamp);
    await SendServiceBusEventAsync(sender, signalTowerId, timestamp);
    Console.WriteLine($"{DateTimeOffset.UtcNow}\tEvents were sent");
}
catch (Exception)
{
    Console.WriteLine("Failed to send events");
}

await eventHubClient.CloseAsync();
Console.ReadLine();

static async Task SendEventHubEventAsync(EventHubProducerClient eventHubClient,
                                   Guid signalTowerId,
                                   DateTimeOffset timestamp)
{
    var datas = new List<EventData>
    {
        CreateEventHubEvent(TimeKeepingOeeEventType.StartShift,
                            signalTowerId,
                            timestamp.AddMinutes(1)),

        CreateEventHubEvent(TimeKeepingOeeEventType.PauseShift,
                            signalTowerId,
                            timestamp.AddMinutes(2)),
        CreateEventHubEvent(TimeKeepingOeeEventType.ResumeShift,
                            signalTowerId,
                            timestamp.AddMinutes(4)),

        CreateEventHubEvent(TimeKeepingOeeEventType.StartDeviation,
                            signalTowerId,
                            timestamp.AddMinutes(3),
                            CalculationMode.Additive),
        CreateEventHubEvent(TimeKeepingOeeEventType.EndDeviation,
                            signalTowerId,
                            timestamp.AddMinutes(5),
                            CalculationMode.Additive),

        CreateEventHubEvent(TimeKeepingOeeEventType.PauseShift,
                            signalTowerId,
                            timestamp.AddMinutes(6)),
        CreateEventHubEvent(TimeKeepingOeeEventType.ResumeShift,
                            signalTowerId,
                            timestamp.AddMinutes(8)),

        CreateEventHubEvent(TimeKeepingOeeEventType.StartDeviation,
                            signalTowerId,
                            timestamp.AddMinutes(7),
                            CalculationMode.Subtractive),
        CreateEventHubEvent(TimeKeepingOeeEventType.EndDeviation,
                            signalTowerId,
                            timestamp.AddMinutes(9),
                            CalculationMode.Subtractive),

        CreateEventHubEvent(TimeKeepingOeeEventType.EndShift,
                            signalTowerId,
                            timestamp.AddMinutes(10)),
    };

    await eventHubClient.SendAsync(datas, new SendEventOptions
    {
        PartitionKey = signalTowerId.ToString(),
    });
}

static EventData CreateEventHubEvent(TimeKeepingOeeEventType eventType, Guid signalTowerId, DateTimeOffset timestamp, CalculationMode? deviationMode = null)
{
    var message = new OeeEventHubMessage(signalTowerId, new TimeKeepingOeeEvent
    {
        UtcTimestamp = timestamp,
        EventType = eventType,
        DeviationMode = deviationMode,
    });

    return new EventData(JsonSerializer.Serialize(message))
    {
        ContentType = nameof(OeeEventHubMessage) + "/json",
        MessageId = message.GetMessageId(),
    };
}

static async Task SendServiceBusEventAsync(ServiceBusSender sender, Guid signalTowerId, DateTimeOffset timestamp)
{
    var taskList = new List<Task> {

        SendServiceBusMessage(
            signalTowerId,
            sender,
            true,
            timestamp),
        SendServiceBusMessage(
            signalTowerId,
            sender,
            false,
            timestamp.AddMinutes(3)),
        SendServiceBusMessage(
            signalTowerId,
            sender,
            true,
            timestamp.AddMinutes(5)),
        SendServiceBusMessage(
            signalTowerId,
            sender,
            false,
            timestamp.AddMinutes(8)),
        SendServiceBusMessage(
            signalTowerId,
            sender,
            true,
            timestamp.AddMinutes(10)),

    };
    await Task.WhenAll(taskList);
}

static async Task SendServiceBusMessage(Guid signalTowerId, ServiceBusSender sender, bool hasAttribute, DateTimeOffset receivedAt)
{
    var liveDataMessage = new LiveDataTopicMessage
    {
        TenantId = Guid.NewGuid(),
        DeviceId = signalTowerId,

        Timestamp = receivedAt,

        HasOeeConfig = true,
        HasOeeAttribute = hasAttribute,
        AvailabilityMode = CalculationMode.Additive,
    };
    await sender.ScheduleMessageAsync(new ServiceBusMessage(JsonSerializer.Serialize(liveDataMessage)), receivedAt);
}