namespace Connect4.Dtos
{
    public record class UpdateGameDto(
        string Name,
        string Genre,
        decimal Price,
        DateTime ReleaseDate
    );
}