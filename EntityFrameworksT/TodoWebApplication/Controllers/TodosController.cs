using Microsoft.AspNetCore.Mvc;
using TodoWebApplication.Models.Requests;
using TodoWebApplication.Services;

namespace TodoWebApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class TodosController : ControllerBase
{

    private readonly ITodosService _todosService;

    public TodosController(ITodosService todosService)
    {
        _todosService = todosService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]
    CreateTodoRequest request)
    {
        var result = await _todosService.Create(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        try
        {
            var result = await _todosService.Get(id);
            return Ok(result);
        }
        catch (KeyNotFoundException e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] bool? check)
    {
        var result = await _todosService.Search(check);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTodoRequest request)
    {
        try
        {
            var result = await _todosService.Update(id, request);
            return Ok(result);
        }
        catch (KeyNotFoundException e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] Guid id) => await _todosService.Delete(id);
}
