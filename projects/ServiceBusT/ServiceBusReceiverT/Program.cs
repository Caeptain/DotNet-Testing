using Azure.Messaging.ServiceBus;

internal class ServiceBusListener
{
    private const string ServiceBusConnectionString = "";
    private const string QueueName = "check-device-disconnected-messages";

    private static async Task Main()
    {
        // Create an instance of the ServiceBusClient
        await using var client = new ServiceBusClient(ServiceBusConnectionString);

        // Create a Processor to receive and process messages
        var processor = client.CreateProcessor(QueueName, new ServiceBusProcessorOptions());

        // Register the message handler and error handler
        processor.ProcessMessageAsync += ProcessMessageAsync;
        processor.ProcessErrorAsync += ProcessErrorAsync;

        // Start processing messages
        await processor.StartProcessingAsync();

        Console.WriteLine("Listening for messages. Press any key to stop...");
        Console.ReadKey();

        // Stop processing messages
        await processor.StopProcessingAsync();
    }

    private static async Task ProcessMessageAsync(ProcessMessageEventArgs args)
    {
        // Process the received message
        var messageBody = args.Message.Body.ToString();
        Console.WriteLine($"Received message: {messageBody}");

        // Complete the message to remove it from the queue
        await args.CompleteMessageAsync(args.Message);
    }

    private static Task ProcessErrorAsync(ProcessErrorEventArgs args)
    {
        // Handle any errors that occur during processing
        Console.WriteLine($"Error occurred: {args.Exception}");
        return Task.CompletedTask;
    }
}
