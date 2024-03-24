using Newtonsoft.Json;

namespace ProClubsBot.Models;

public class Player
{
    public Player(string name, int overallRating, float averageRating, int appearances, int goals,
        int shotSuccessRate, int assists, int passes, int passSuccessRate, int tackles, int tackleSuccessRate, int redCards)
    {
        Name = name;
        OverallRating = overallRating;
        AverageRating = averageRating;
        Appearances = appearances;
        Goals = goals;
        ShotSuccessRate = shotSuccessRate;
        Assists = assists;
        Passes = passes;
        PassSuccessRate = passSuccessRate;
        Tackles = tackles;
        TackleSuccessRate = tackleSuccessRate;
        RedCards = redCards;
    }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("proOverall")]
    public int OverallRating { get; set; }

    [JsonProperty("ratingAve")]
    public float AverageRating { get; set; }

    [JsonProperty("gamesPlayed")]
    public int Appearances { get; set; }

    [JsonProperty("goals")]
    public int Goals { get; set; }

    [JsonProperty("shotSuccessRate")]
    public int ShotSuccessRate { get; set; }

    [JsonProperty("assists")]
    public int Assists { get; set; }

    [JsonProperty("passesMade")]
    public int Passes { get; set; }

    [JsonProperty("passSuccessRate")]
    public int PassSuccessRate { get; set; }

    [JsonProperty("tacklesMade")]
    public int Tackles { get; set; }

    [JsonProperty("tackleSuccessRate")]
    public int TackleSuccessRate { get; set; }

    [JsonProperty("redCards")]
    public int RedCards { get; set; }

    public override string ToString()
    {
        return $"{Name} - Overall: {OverallRating}, Apps: {Appearances}, " +
            $"Avg: {AverageRating}, Goals: {Goals}, " +
            $"Shot (%): {ShotSuccessRate}, Assist: {Assists}, " +
            $"Passes: {Passes}, Pass (%): {PassSuccessRate}, " +
            $"Tackles: {Tackles}, Tackle (%): {TackleSuccessRate}, " +
            $"Red cards: {RedCards}";
    }
}
