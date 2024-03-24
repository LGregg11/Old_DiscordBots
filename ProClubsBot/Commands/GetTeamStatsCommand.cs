using ConsoleTables;
using Discord.WebSocket;
using DiscordBotCommon;
using ProClubsBot.Models;
using System.Reflection;

namespace ProClubsBot.Commands;

internal class GetTeamStatsCommand : ICommand
{
    private readonly ProClubsApiClient apiClient;

    public GetTeamStatsCommand(ProClubsApiClient apiClient)
    {
        this.apiClient = apiClient;
    }

    public string Name => "team-stats";

    public string Description => "Return the team stats";

    public CommandType CommandType => CommandType.Global;

    public IList<IOption> Options => new List<IOption>();

    public bool IsRegistered { get; set; }

    public void HandleCommand(SocketSlashCommand socketSlashCommand)
    {
        Team? teamResponse = apiClient.GetTeamStats();
        if (teamResponse is not Team team)
        {
            socketSlashCommand.RespondAsync("Could not find team");
            return;
        }

        string? teamNameResponse = apiClient.GetTeamNameFromClubId();

        if (teamNameResponse is not string teamName)
        {
            socketSlashCommand.RespondAsync("Could not find team name");
            return;
        }

        team.Name = teamName;
        socketSlashCommand.RespondAsync($"`{team}`");
    }
}
