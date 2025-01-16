using System.ComponentModel.DataAnnotations;

public class CreateGameDto
{
    [Required]
    public string Name { get; set; }
}
