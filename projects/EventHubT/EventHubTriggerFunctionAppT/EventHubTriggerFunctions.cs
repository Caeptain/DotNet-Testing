using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace EventHubTriggerFunctionAppT;

public class EventHubTriggerFunctions
{
    private readonly ILogger<EventHubTriggerFunctions> _logger;

    public EventHubTriggerFunctions(ILogger<EventHubTriggerFunctions> logger)
    {
        _logger = logger;
    }

    [Function(nameof(EventHubTriggerFunction))]
    public void EventHubTriggerFunction([EventHubTrigger("", Connection = "EventHubListenConnection")]
    EventData[] events)
    {
        foreach (var @event in events)
        {
            _logger.LogInformation("Event Content-Type: {contentType}", @event.ContentType);
            _logger.LogInformation("Event Body: {body}", @event.Body);
        }
    }
}
