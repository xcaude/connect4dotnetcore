using System.ComponentModel.DataAnnotations;

public class RegisterDto
{
    [Required]
    public string Login { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}