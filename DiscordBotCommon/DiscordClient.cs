using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.Linq;

namespace DiscordBotCommon;

public class DiscordClient : IDiscordClient
{
    private readonly DiscordSocketClient client;

    public event Action<LogMessage> Log;
    public event Action Ready;
    public event Action<SocketSlashCommand> CommandReceived;

    public DiscordClient(DiscordToken discordToken)
    {
        DiscordToken = discordToken;
        
        client = new DiscordSocketClient(new DiscordSocketConfig
        {
            LogLevel = LogSeverity.Info,
            GatewayIntents = GatewayIntents.Guilds
        });

        client.Log += logMessage =>
        {
            Log?.Invoke(logMessage);
            return Task.CompletedTask;
        };

        client.Ready += () =>
        {
            Ready?.Invoke();
            return Task.CompletedTask;
        };

        client.ApplicationCommandCreated += cmd =>
        {
            Console.WriteLine($"{cmd.Name} command created");
            return Task.CompletedTask;
        };

        Console.WriteLine("discord client created");
    }

    public DiscordToken DiscordToken { get; private set; }

    public void Start()
    {
        Console.WriteLine("Starting discord client");

        client.LoginAsync(DiscordToken.TokenType, DiscordToken.Token).Wait();
        client.StartAsync().Wait();

        client.SlashCommandExecuted += socketSlashCommand =>
        {
            CommandReceived?.Invoke(socketSlashCommand);
            return Task.CompletedTask;
        };

        Console.WriteLine("Discord client started");
    }

    public void TryCreateGlobalCommand(SlashCommandProperties slashCommandProperties)
    {
        try
        {
            var result = client.CreateGlobalApplicationCommandAsync(slashCommandProperties).Result;
            Console.WriteLine($"Command created: {result.Name}");
        }
        catch (HttpException exception)
        {
            var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}
