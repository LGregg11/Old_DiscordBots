using Discord.WebSocket;

namespace DiscordBotCommon
{
    public interface ICommandController
    {
        public void AddCommands(IList<ICommand> commands);
        public void RegisterCommands();
        public void HandleSocketSlashCommand(SocketSlashCommand socketSlashCommand); // TODO: Generic?
    }
}