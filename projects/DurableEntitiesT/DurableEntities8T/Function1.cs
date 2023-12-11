using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;
using Microsoft.DurableTask.Entities;
using Microsoft.Extensions.Logging;

namespace DurableEntities8T;

public partial class EntityTester
{
    private readonly ILogger _logger;

    public EntityTester(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<EntityTester>();
    }

    [Function(nameof(TestEntities))]
    public static async Task<HttpResponseData> TestEntities(
      [HttpTrigger(AuthorizationLevel.Function)] HttpRequestData req,
      int amount,
      [DurableClient] DurableTaskClient client)
    {
        var entityId = new EntityInstanceId(nameof(Counter), "myCounter");
        if (amount > 0)
        {
            await client.Entities.SignalEntityAsync(entityId, "Add", amount);
        }

        var entity = await client.Entities.GetEntityAsync<int>(entityId);

        if (entity is null)
        {
            return req.CreateResponse(HttpStatusCode.NotFound);
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(entity);

        return response;
    }
}
