namespace DiscordBotCommon;

public static class DiscordStringExtensions
{
    public static string ToCodeBlockString(this string str) => $"```{str}```";
}
