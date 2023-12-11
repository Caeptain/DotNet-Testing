using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask.Entities;
using Microsoft.Extensions.Logging;

namespace DurableEntities8T;

public class Counter : TaskEntity<int>
{
    private readonly ILogger logger;

    public Counter(ILogger<Counter> logger)
    {
        this.logger = logger;
    }

    public async Task Add(int amount) => Task.FromResult(State += amount);

    public void Reset() => State = 0;

    public int Get() => State;

    [Function(nameof(Counter))]
    public Task RunEntityAsync([EntityTrigger] TaskEntityDispatcher dispatcher)
        => dispatcher.DispatchAsync(this);
}