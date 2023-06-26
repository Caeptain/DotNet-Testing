using System.Text.Json;
using Azure.Messaging.ServiceBus;

var client = new ServiceBusClient("Endpoint=sb://sb-hwcs-test-gwc.servicebus.windows.net/;SharedAccessKeyName=IoTHubFunctionApp;SharedAccessKey=L3w4W631Qid+KwP02+vtgNrbhOpOzC9lfaF8Q0J2FjA=;EntityPath=update-radionetwork-job-messages");
var sender = client.CreateSender("update-radionetwork-job-messages");

//await MicrosoftSolution(client, sender, 3);
await OurSolution(sender);

static async Task MicrosoftSolution(ServiceBusClient client, ServiceBusSender sender, int NumOfMessages)
{
    var messageBatch = await sender.CreateMessageBatchAsync();

    for (var i = 1; i <= NumOfMessages; i++)
    {
        if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message Test {i}")))
        {
            throw new NotImplementedException($"The message {i} is too large to fit in the batch.");
        }
    }

    try
    {
        await sender.SendMessagesAsync(messageBatch);
        Console.WriteLine($"A batch of {NumOfMessages} messages has been published to the queue.");
    }
    finally
    {
        await sender.DisposeAsync();
        await client.DisposeAsync();
    }

    Console.WriteLine("Press any key to end the application");
    Console.ReadKey();
}

static async Task OurSolution(ServiceBusSender sender)
{
    var messageBody = JsonSerializer.Serialize("{\"radioNetworkId\": 10}");
    var message = new ServiceBusMessage(messageBody);
    await sender.SendMessageAsync(message);
}