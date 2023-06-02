namespace TodoWebApi.Models.Requests;
public class UpdateTodoRequest
{
    public string? Title { get; set; }
    public bool? Check { get; set; }
}
