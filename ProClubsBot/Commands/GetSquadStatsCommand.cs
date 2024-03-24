using ConsoleTables;
using Discord.WebSocket;
using DiscordBotCommon;
using ProClubsBot.Models;

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
        
        ConsoleTable table = new("Name", "Apps", "Avg", "Goals", "Shot (%)", "Assists", "Passes", "Pass (%)", "Tackles", "Tackle (%)", "Reds");
        foreach (Player p in squad.Players)
            table.AddRow(p.Name, p.Appearances, p.AverageRating, p.Goals, p.ShotSuccessRate, p.Assists, p.Passes, p.PassSuccessRate, p.Tackles, p.TackleSuccessRate, p.RedCards);

        string tableString = table.ToMinimalString();
        socketSlashCommand.RespondAsync($"`{tableString}`");
    }
}
