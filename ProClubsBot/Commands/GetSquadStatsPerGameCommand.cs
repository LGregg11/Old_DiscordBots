using ConsoleTables;
using Discord.WebSocket;
using DiscordBotCommon;
using ProClubsBot.Models;

namespace ProClubsBot.Commands;

internal class GetSquadStatsPerGameCommand : ICommand
{
    private readonly ProClubsApiClient apiClient;

    public GetSquadStatsPerGameCommand(ProClubsApiClient apiClient)
    {
        this.apiClient = apiClient;
    }

    public string Name => "squad-stats-per-game";

    public string Description => "Return a list of all the squads' stats per game average";

    public CommandType CommandType => CommandType.Global;

    public IList<IOption> Options => new List<IOption>();

    public bool IsRegistered { get; set; }

    public void HandleCommand(SocketSlashCommand socketSlashCommand)
    {
        Squad? squad = apiClient.GetSquadStats();
        if (squad == null)
        {
            socketSlashCommand.RespondAsync("Could not find squad");
            return;
        }

        ConsoleTable table = new("Name", "Apps", "Rating", "Goals", "Assists", "Passes", "Tackles");
        foreach (Player p in squad.Players)
        {
            float apps = p.Appearances * 1.0f;

            table.AddRow(p.Name, p.Appearances, p.AverageRating,
                ToTwoDPString(p.Goals / apps), ToTwoDPString(p.Assists / apps),
                ToTwoDPString(p.Passes / apps), ToTwoDPString(p.Tackles / apps));
        }

        string tableString = table.ToMinimalString();
        socketSlashCommand.RespondAsync($"`{tableString}`");
    }

    private static string ToTwoDPString(float number) => $"{number:F2}";
}