using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

[DurableTask(nameof(Counter))]
public class Counter : TaskActivity<int, int>
{
    private readonly ILogger logger;
    public int Count { get; set; }

    public Counter(ILogger<Counter> logger)
    {
        this.logger = logger;
    }

    public override Task<int> RunAsync(TaskActivityContext context, int input)
    {
        logger.LogInformation($"Counter received {input}");
        Count++;
        return Task.FromResult(Count);
    }
}
