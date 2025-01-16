using System.ComponentModel.DataAnnotations;

public class JoinGameDto
{
    [Required]
    public string GuestId { get; set; }
}
