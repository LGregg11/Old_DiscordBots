using ProClubsBot.Models;

namespace ProClubsBot.Commands;

internal class GetSquadStatsOrderByCommandOption : OrderByCommandOption
{
    public override IList<string> Choices => typeof(Player).GetProperties().Select(p => p.Name).ToList();
}
