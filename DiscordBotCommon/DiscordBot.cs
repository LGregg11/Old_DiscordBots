using Discord;

namespace DiscordBotCommon;

public class DiscordBot
{
    private readonly IDiscordClient client;
    private readonly ICommandController commandController;

    public DiscordBot(IDiscordClient client, ICommandController commandController)
    {
        this.client = client;
        this.commandController = commandController;

        client.Log += Log;
        client.Ready += OnClientReady;
    }

    public void Start()
    {
        client.Start();
    }

    private void OnClientReady()
    {
        commandController.RegisterCommands();
        client.CommandReceived += commandController.HandleSocketSlashCommand;
    }

    private static void Log(LogMessage log)
    {
        switch (log.Severity)
        {
            case LogSeverity.Critical:
            case LogSeverity.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case LogSeverity.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case LogSeverity.Info:
                Console.ForegroundColor = ConsoleColor.White;
                break;
            case LogSeverity.Verbose:
            case LogSeverity.Debug:
                Console.ForegroundColor = ConsoleColor.DarkGray;
                break;
        }

        Console.WriteLine($"{DateTime.Now,-19} [{log.Severity}] {log.Source}: {log.Message} {log.Exception}");
        Console.ResetColor();
    }
}
