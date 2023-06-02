using TodoWebApplication.Models.Requests;
using TodoWebApplication.Models.Responces;

namespace TodoWebApplication.Services;

public interface ITodosService
{
    Task<GetTodoResponce> Create(CreateTodoRequest request);
    Task<GetTodoResponce> Get(Guid id);
    Task<IEnumerable<GetTodoResponce>> Search(bool? check);
    Task<GetTodoResponce> Update(Guid id, UpdateTodoRequest request);
    Task Delete(Guid id);
}