using Microsoft.EntityFrameworkCore;

namespace Connect4.Data;
public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<Connect4DbContext>();
        dbContext.Database.Migrate();
    }
}
