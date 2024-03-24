using Discord;
using Discord.WebSocket;

namespace DiscordBotCommon;

/// <remarks>
/// Note that only global commands are supported at the moment
/// </remarks>
public class CommandController : ICommandController
{
    private readonly IList<ICommand> commands;
    private readonly IDiscordClient client;

    public CommandController(IDiscordClient client)
    {
        this.client = client;
        commands = new List<ICommand>();
    }

    public void AddCommands(IList<ICommand> commands)
    {
        foreach (ICommand command in commands)
        {
            if (!this.commands.Contains(command))
                this.commands.Add(command);
        }
    }

    public void RegisterCommands()
    {
        Console.WriteLine("Registering commands..");

        foreach (ICommand command in GetUnregisteredCommands().ToList())
            RegisterCommand(command);

        Console.WriteLine("Commands registered");
    }

    public void HandleSocketSlashCommand(SocketSlashCommand socketSlashCommand)
    {
        if (TryGetRegisteredCommand(socketSlashCommand.Data.Name, out ICommand command))
        {
            Console.WriteLine($"Handling {socketSlashCommand.Data.Name}..");
            command.HandleCommand(socketSlashCommand);
        }
    }

    private IEnumerable<ICommand> GetUnregisteredCommands() => commands.Where(c => !c.IsRegistered);

    private void RegisterCommand(ICommand command)
    {
        // For now, if not global, don't register.
        // If command already registered or creation of command fails, return false
        if (command.CommandType != CommandType.Global || command.IsRegistered)
            return;

        CreateGlobalCommand(command);
        command.IsRegistered = true;
        commands.Add(command);
    }

    private bool TryGetRegisteredCommand(string commandName, out ICommand command)
    {
        command = commands.FirstOrDefault(c => c.Name == commandName);
        return command != null;
    }

    private void CreateGlobalCommand(ICommand command)
    {
        SlashCommandBuilder commandBuilder = new();
        commandBuilder.WithName(command.Name);
        commandBuilder.WithDescription(command.Description);
        foreach (IOption option in command.Options)
            commandBuilder.AddOption(option.Name, option.OptionType, option.Description, option.IsRequired);

        client.TryCreateGlobalCommand(commandBuilder.Build());
    }
}
