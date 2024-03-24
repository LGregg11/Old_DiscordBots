using ApiClient;
using Discord;
using DiscordBotCommon;
using ProClubsBot;

DiscordToken discordToken = new DiscordToken(File.ReadAllText("ProClubsToken.txt"), TokenType.Bot);

DiscordBotCommon.IDiscordClient client = new DiscordClient(discordToken);
ICommandController commandController = new CommandController(client);
IApiClient apiClient = new ApiClient.ApiClient();

ProClubsDiscordBot bot = new(client, commandController, apiClient);

// Do bot things here
bot.Start();

// Block this task until the program is closed.
await Task.Delay(-1);