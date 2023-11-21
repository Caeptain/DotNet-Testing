using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

[DurableTask(nameof(MyOrchestration))]
public class MyOrchestration : TaskOrchestrator<int, int>
{
    public override async Task<int> RunAsync(TaskOrchestrationContext context, int input)
    {
        var logger = context.CreateReplaySafeLogger<MyOrchestration>();
        logger.LogInformation($"MyOrchestration received {input}");

        return await context.CallCounterAsync(input);
    }
}