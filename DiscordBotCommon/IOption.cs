using Discord;

namespace DiscordBotCommon;

public interface IOption
{
    public string Name { get; }
    public ApplicationCommandOptionType OptionType { get; }
    public string Description { get; }
    public bool IsRequired { get; }
    public IList<string> Choices { get; }
}