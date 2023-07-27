using FluentValidation;
using TodoWebApi.Extensions;
using TodoWebApi.Models.Requests;

namespace TodoWebApi.Models.Validations;

public class CreateTodoRequestValidation<T> : AbstractValidator<T> where T : CreateTodoRequest
{
    public CreateTodoRequestValidation()
    {
        RuleFor(x => x.Title).MustBeValidTitle().NotEmpty();
    }
}
