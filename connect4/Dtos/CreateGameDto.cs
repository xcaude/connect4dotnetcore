using System.ComponentModel.DataAnnotations;

public class CreateGameDto
{
    [Required]
    public string HostId { get; set; }
}
