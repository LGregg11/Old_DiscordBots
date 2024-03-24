using ApiClient;
using DiscordBotCommon;
using ProClubsBot.Commands;

namespace ProClubsBot;

internal class ProClubsDiscordBot : DiscordBot
{
    private readonly ProClubsApiClient apiClient;

    private readonly IList<ICommand> commands;

    public ProClubsDiscordBot(IDiscordClient discordClient, ICommandController commandController, IApiClient apiClient) : 
        base(discordClient, commandController)
    {
        this.apiClient = new ProClubsApiClient(apiClient);

        commands = new List<ICommand>
        {
            new HelloWorldCommand(),
            new GetSquadStatsCommand(this.apiClient),
            new GetTeamStatsCommand(this.apiClient),
            new GetSquadStatsPerGameCommand(this.apiClient)
        };

        commandController.AddCommands(commands);
    }


}
