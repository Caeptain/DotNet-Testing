namespace TodoWebApi.Models.Responces;

public class GetTodoResponce
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ClosingDate { get; set; }
    public string? Title { get; set; }
}
