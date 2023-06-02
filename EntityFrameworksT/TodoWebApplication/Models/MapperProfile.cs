using AutoMapper;
using EntityFrameworkLib.Entities;
using TodoWebApplication.Models.Requests;
using TodoWebApplication.Models.Responces;

namespace TodoWebApplication.Models;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<TodoEntity, GetTodoResponce>();
        CreateMap<CreateTodoRequest, TodoEntity>();
    }
}
