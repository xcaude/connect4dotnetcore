using System.Net.Http;
using Connect4Game.Models;

namespace Connect4Game.ClientWeb.Data.Services;

public class GameService(HttpClient client) : ServiceBase<GameResource, GameListItem, GameItem>(client, "Game")
{
    public async Task<GameListItem[]> GetGamesAsync(
        int limit = 10,
        int offset = 0)
    {
        var request = MakePaginatedGetAllRequest(limit, offset);

        var games = await SendGetAllPaginatedRequest(request);
        return games;
    }

    public Task<GameItem?> GetGameAsync(int id)
    {
        var request = MakeGetOneRequest(id);

        return SendGetOneRequest(request);
    }

    public Task<int> AddGameAsync(GameResource resource)
    {
        var request = MakeAddRequest(resource);
        return SendAddRequest(request);
    }

    public Task<int> EditGameAsync(int id, GameResource resource)
    {
        var request = MakeUpdateRequest(id, resource);
        return SendAddRequest(request);
    }

    public Task<bool> FinishGameAsync(int id)
    {
        var request = MakeActionRequest(id, "finish");
        return SendActionRequest(request);
    }

    public Task<bool> PinGameAsync(int id)
    {
        var request = MakeActionRequest(id, "pin");
        return SendActionRequest(request);
    }

    public Task<bool> UnpinGameAsync(int id)
    {
        var request = MakeActionRequest(id, "unpin");
        return SendActionRequest(request);
    }


}