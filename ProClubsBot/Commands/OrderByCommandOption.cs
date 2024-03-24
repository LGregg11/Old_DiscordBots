using Discord;
using DiscordBotCommon;

namespace ProClubsBot.Commands;

internal abstract class OrderByCommandOption : IOption
{
    public string Name => "order";

    public ApplicationCommandOptionType OptionType => ApplicationCommandOptionType.String;

    public string Description => "Choose which column to order by (DESC)";

    public bool IsRequired => false;

    public abstract IList<string> Choices { get; }
}
