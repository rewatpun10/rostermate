using System.ComponentModel.DataAnnotations;

namespace RosterMate.Domain.Entities;

public class Staff
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
}
