using System.Text.Json;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

var eventHubConnectionString = "";

var producerClient = new EventHubProducerClient(eventHubConnectionString);

await SendEventAsync(producerClient);
await producerClient.CloseAsync();

static async Task SendEventAsync(EventHubProducerClient producerClient)
{
    var ed = new EventData(JsonSerializer.Serialize(new GatewayConnectedEvent
    {
        GatewayId = Guid.NewGuid(),
        Timestamp = DateTimeOffset.UtcNow
    }))
    {
        ContentType = "GatewayConnectedEvent/json"
    };
    try
    {
        await producerClient.SendAsync(new[] { ed }, new SendEventOptions
        {
            PartitionKey = "43a2b065-6765-4150-b8da-1777348eb91c"
        });
        Console.WriteLine("Event sent successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error sending event: {ex.Message}");
    }
}

public class GatewayConnectedEvent
{
    public Guid GatewayId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public string MessageId { get; set; }

    public GatewayConnectedEvent()
    {
        MessageId = Guid.NewGuid().ToString();
    }
}
