using System.Transactions;

using AutoMapper;
using CoreLib;
using EntityFrameworkLib.Entities;
using TodoWebApplication.Models.Requests;
using TodoWebApplication.Models.Responces;

namespace TodoWebApplication.Services.Impl;

public class TodosService : ITodosService
{

    private readonly IRepository<TodoEntity> _repository;
    private readonly IMapper _mapper;

    public TodosService(IRepository<TodoEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<GetTodoResponce> Create(CreateTodoRequest request)
    {
        var requestEntity = new TodoEntity() { Title = request.Title, CreationDate = DateTime.Now };
        var entity = await _repository.InsertAsync(requestEntity);
        var result = _mapper.Map<GetTodoResponce>(entity);
        return result;
    }

    public async Task<GetTodoResponce> Get(Guid id)
    {
        var entity = await _repository.GetAsyc(id);
        if (entity is null) throw new KeyNotFoundException(string.Format("No Entity with id: {0} was found", id));
        var result = _mapper.Map<GetTodoResponce>(entity);
        return result;
    }

    public async Task<IEnumerable<GetTodoResponce>> Search(bool? check)
    {
        IEnumerable<TodoEntity> entities = check.HasValue
            ? entities = _repository.AsQueryable.Where(x => x.ClosingDate.HasValue == check.Value)
            : entities = await _repository.GetAllAsync();

        if (!entities.Any())
            return new List<GetTodoResponce>();

        var result = _mapper.Map<List<GetTodoResponce>>(entities);

        return result;
    }

    public async Task<GetTodoResponce> Update(Guid id, UpdateTodoRequest request)
    {
        using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var entity = await _repository.GetAsyc(id);
        if (entity is null)
            throw new KeyNotFoundException(string.Format("No Entity with id: {0} was found", id));

        if (!string.IsNullOrWhiteSpace(request.Title)) entity.Title = request.Title;

        if (request.Check.HasValue)
            entity.ClosingDate = request.Check.Value ? DateTime.Now : null;

        var responce = await _repository.UpdateAsync(entity);
        var result = _mapper.Map<GetTodoResponce>(responce);

        ts.Complete();
        return result;
    }

    public async Task Delete(Guid id)
    {
        using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var entity = await _repository.GetAsyc(id);
        if (entity == null)
            throw new KeyNotFoundException(string.Format("No Entity with id: {0} was found", id));
        await _repository.RemoveAsync(entity);

        ts.Complete();
    }
}
