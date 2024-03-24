using Discord;
using DiscordBotCommon;

namespace ProClubsBot.Commands
{
    public class HelloWorldCommandOption : IOption
    {
        public string Name => "mention";

        public ApplicationCommandOptionType OptionType => ApplicationCommandOptionType.Mentionable;

        public string Description => "Choose who to mention";

        public bool IsRequired => false;
    }
}
