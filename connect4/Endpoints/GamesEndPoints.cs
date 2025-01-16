using Connect4.Data;
using Connect4.Dtos;
using Connect4.Entities;

namespace Connect4.Endpoints;

/*public static class GamesEnpoints
{
   const string GetGameEndpointName = "GetGame";

   private static readonly List<GameDto> games = [
    new (
        1,
        "Street Fighter",
        "Fight",
        19.99M,
        new DateTime(2021, 12, 31)
    ),
    new (
        2,
        "Mortal Kombat",
        "Fight",
        29.99M,
        new DateTime(2021, 12, 31)
    )
   ];

   public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
   {
        var group = app.MapGroup("/games");
       // GET /games
       group.MapGet("/", () => games);

       // GET /games/{id}
       group.MapGet("/{id}", (int id) =>
       {
            GameDto? game = games.Find(g => g.Id == id);
            return game is not null ? Results.Ok(game) : Results.NotFound();
       }) .WithName(GetGameEndpointName);

       // POST /games
         group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
         {
            Game game = new()
            {
                Name = newGame.Name,
                Genre = dbContext.Genres.Find(newGame.GenreId),
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };
            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            GameDto gameDto = new(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
            );
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, gameDto);
         }).WithParameterValidation();
        //DELETE /games/{id}
        group.MapDelete("/{id}", (int id) =>
        {
            GameDto? game = games.Find(g => g.Id == id);
            if (game is null)
            {
                return Results.NotFound();
            }
            games.Remove(game);
            return Results.NoContent();
        });

        //PUT /games/{id}
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(g => g.Id == id);

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.NoContent();
        });


       return group;
   }
}*/