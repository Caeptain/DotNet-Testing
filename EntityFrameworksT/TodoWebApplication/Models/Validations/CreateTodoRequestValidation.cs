using FluentValidation;
using TodoWebApplication.Extensions;
using TodoWebApplication.Models.Requests;

namespace TodoWebApplication.Models.Validations;

public class CreateTodoRequestValidation<T> : AbstractValidator<T> where T : CreateTodoRequest
{
    public CreateTodoRequestValidation()
    {
        RuleFor(x => x.Title).MustBeValidTitle().NotEmpty();
    }
}
