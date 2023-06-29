using System.Text.Json;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

var eventHubConnectionString = "Endpoint=sb://evhns-hwcs-dev-gwc.servicebus.windows.net/;SharedAccessKeyName=IoTHubReceiverSender;SharedAccessKey=l/19yKwbch7GTQeYVdYkjO0zOc6IwHG7ozTIDSzaqfs=;EntityPath=evh-hwcs-livedata-dev-gwc";

var producerClient = new EventHubProducerClient(eventHubConnectionString);

await SendEventAsync(producerClient);
Console.ReadLine();

static async Task SendEventAsync(EventHubProducerClient producerClient)
{
    var ed = new EventData(JsonSerializer.Serialize(new GatewayConnectedEvent
    {
        GatewayId = Guid.Empty,
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
        Console.WriteLine($"Event sent successfully.");
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
