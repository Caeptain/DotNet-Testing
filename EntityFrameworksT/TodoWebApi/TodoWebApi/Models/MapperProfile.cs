using AutoMapper;
using EntityFrameworkLib.Entities;
using TodoWebApi.Models.Requests;
using TodoWebApi.Models.Responces;

namespace TodoWebApi.Models;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<TodoEntity, GetTodoResponce>();
        CreateMap<CreateTodoRequest, TodoEntity>();
    }
}
