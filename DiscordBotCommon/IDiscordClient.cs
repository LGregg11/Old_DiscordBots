using Discord;
using Discord.WebSocket;

namespace DiscordBotCommon
{
    public interface IDiscordClient
    {
        public event Action<LogMessage> Log;
        public event Action Ready;
        public event Action<SocketSlashCommand> CommandReceived; // TODO: Change SocketSlashCommand to generic command interface
        public DiscordToken DiscordToken { get; }
        public void Start();
        public void TryCreateGlobalCommand(SlashCommandProperties slashCommandProperties);
    }
}
