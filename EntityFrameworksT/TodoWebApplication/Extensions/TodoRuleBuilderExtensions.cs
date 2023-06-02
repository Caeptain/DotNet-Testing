using FluentValidation;

namespace TodoWebApplication.Extensions;
public static class TodoRuleBuilderExtensions
{
    public static IRuleBuilder<T, string?> MustBeValidTitle<T>(this IRuleBuilder<T, string?> builder)
    {
        builder.MinimumLength(2);
        builder.MaximumLength(20);
        return builder;
    }
}
