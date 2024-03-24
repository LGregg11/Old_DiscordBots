using ConsoleTables;
using Discord.WebSocket;
using DiscordBotCommon;
using ProClubsBot.Models;
using System.Reflection;

namespace ProClubsBot.Commands;

internal class GetSquadStatsCommand : ICommand
{
    private readonly ProClubsApiClient apiClient;

    public GetSquadStatsCommand(ProClubsApiClient apiClient)
    {
        this.apiClient = apiClient;
    }

    public string Name => "squad-stats";

    public string Description => "Return a list of all the squads' stats";

    public CommandType CommandType => CommandType.Global;

    public IList<IOption> Options => new List<IOption> { new GetSquadStatsOrderByCommandOption() };

    public bool IsRegistered { get; set; }

    public void HandleCommand(SocketSlashCommand socketSlashCommand)
    {
        Squad? squad = apiClient.GetSquadStats();
        if (squad == null)
        {
            socketSlashCommand.RespondAsync("Could not find squad");
            return;
        }

        List<Player> players = squad.Players;

        if (socketSlashCommand.Data.Options.FirstOrDefault()?.Value is string value &&
            typeof(Player).GetProperty(value) is PropertyInfo propertyInfo)
        {
            players = players.OrderByDescending(p => propertyInfo.GetValue(p, null)).ToList();
        }

        string[] columnHeaders = GetPropertiesWithNameAttribute<Player>()
            .Select(prop => prop.GetCustomAttribute<NameAttribute>().Name)
            .Where(n => n is not null)
            .ToArray();

        ConsoleTable table = new(columnHeaders);
        foreach (Player p in players)
        {
            table.AddRow(
                GetPropertiesWithNameAttribute<Player>()
                .Select(prop => prop.GetValue(p, null))
                .Where(v => v is not null)
                .ToArray());
        }

        socketSlashCommand.RespondAsync(table.ToMinimalString().ToCodeBlockString());
    }

    private IEnumerable<PropertyInfo> GetPropertiesWithNameAttribute<T>() =>
        typeof(T).GetProperties().Where(prop => prop.GetCustomAttribute<NameAttribute>() is not null);
}
