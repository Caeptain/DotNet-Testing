using FluentValidation;
using TodoWebApi.Extensions;
using TodoWebApi.Models.Requests;

namespace TodoWebApi.Models.Validations;
public class UpdateTodoRequestValidation<T> : AbstractValidator<T> where T : UpdateTodoRequest
{
    public UpdateTodoRequestValidation()
    {
        RuleFor(x => x.Title).MustBeValidTitle();
    }
}
