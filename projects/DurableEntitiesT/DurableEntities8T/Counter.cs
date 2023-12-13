using Microsoft.Azure.Functions.Worker;

namespace DurableEntities8T;

public class Counter
{
    public int CurrentValue { get; set; }

    public void Add(int amount) => CurrentValue += amount;

    public void Reset() => CurrentValue = 0;

    public int Get() => CurrentValue;

    [Function(nameof(Counter))]
    public static Task RunEntityAsync([EntityTrigger] TaskEntityDispatcher dispatcher)
        => dispatcher.DispatchAsync<Counter>();
}
