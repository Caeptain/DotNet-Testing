using System.Text;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;

var eventHubConnectionString = "";
var consumerGroup = "monitoring-dietz";

var consumerClient = new EventHubConsumerClient(consumerGroup, eventHubConnectionString);

static async Task ProcessEventAsync(EventData eventData)
{
    if (eventData.ContentType == "GatewayConnectedEvent/json" || eventData.ContentType == "GatewayDisconnectedEvent/json")
    {
        var stringMessage = Encoding.UTF8.GetString(eventData.Body.ToArray());
        var searchString = Guid.Empty.ToString();
        if (stringMessage.Contains(searchString))
        {
            Console.WriteLine($"{eventData.ContentType} Received event: {Encoding.UTF8.GetString(eventData.Body.ToArray())}");
        }
    }
}

var cancellationSource = new CancellationTokenSource();
try
{
    await foreach (var partitionEvent in consumerClient.ReadEventsAsync(cancellationSource.Token))
    {
        await ProcessEventAsync(partitionEvent.Data);
    }
}
catch (TaskCanceledException)
{
    // This exception is expected when the cancellation token is canceled.
    Console.WriteLine("Event receiving has been canceled.");
}

Console.ReadLine();
