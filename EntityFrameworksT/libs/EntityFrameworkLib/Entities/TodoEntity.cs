using System.ComponentModel.DataAnnotations;
using CoreLib;

namespace EntityFrameworkLib.Entities;

public class TodoEntity : Entity
{
    [Required]
    public string? Title { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ClosingDate { get; set; }
}
