using Discord.WebSocket;

namespace DiscordBotCommon;

public enum CommandType
{
    Global,
    Guild // TODO
}

public interface ICommand
{
    public string Name { get; }
    public string Description { get; }
    public CommandType CommandType { get; }
    public IList<IOption> Options { get; }
    public bool IsRegistered { get; set; }
    public void HandleCommand(SocketSlashCommand socketSlashCommand);
}
