using FluentValidation;
using TodoWebApplication.Extensions;
using TodoWebApplication.Models.Requests;

namespace TodoWebApplication.Models.Validations;
public class UpdateTodoRequestValidation<T> : AbstractValidator<T> where T : UpdateTodoRequest
{
    public UpdateTodoRequestValidation()
    {
        RuleFor(x => x.Title).MustBeValidTitle();
    }
}
