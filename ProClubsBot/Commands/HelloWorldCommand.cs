using Discord.WebSocket;
using DiscordBotCommon;

namespace ProClubsBot.Commands
{
    public class HelloWorldCommand : ICommand
    {
        public HelloWorldCommand()
        {
        }

        public string Name => "hello-world";

        public string Description => "This returns hello world";

        public CommandType CommandType => CommandType.Global;

        public IList<IOption> Options => new List<IOption>(); // { new HelloWorldCommandOption() };

        public bool IsRegistered { get; set; }

        public void HandleCommand(SocketSlashCommand socketSlashCommand)
        {
            socketSlashCommand.RespondAsync("Hello world").Wait();
        }
    }
}
