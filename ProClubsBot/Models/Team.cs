using Newtonsoft.Json;

namespace ProClubsBot.Models;

public class Team
{
    public Team(string name, int games, int wins, int draws, int losses, int goals, int goalsAgainst)
    {
        Name = name;
        Games = games;
        Wins = wins;
        Draws = draws;
        Losses = losses;
        Goals = goals;
        GoalsAgainst = goalsAgainst;
    }

    public string Name { get; set; } = "Team";

    [JsonProperty("gamesPlayed")]
    public int Games { get; set; }

    [JsonProperty("wins")]
    public int Wins { get; set; }

    [JsonProperty("ties")]
    public int Draws { get; set; }

    [JsonProperty("losses")]
    public int Losses { get; set; }

    [JsonProperty("goals")]
    public int Goals { get; set; }

    [JsonProperty("goalsAgainst")]
    public int GoalsAgainst { get; set; }

    public override string ToString()
    {
        return $"{Name} - Games: {Games} | Record: {Wins}-{Draws}-{Losses} | Goals: {Goals} | Goals Against: {GoalsAgainst}";
    }
}
