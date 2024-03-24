using Newtonsoft.Json;

namespace ProClubsBot.Models;

internal class TeamInfo
{
    public TeamInfo(string name)
    {
        Name = name;
    }

    [JsonProperty("name")]
    public string Name { get; set; }

    public override string ToString() => Name;
}
