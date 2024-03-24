using Newtonsoft.Json;

namespace ProClubsBot.Models;

[AttributeUsage(AttributeTargets.Property)]
internal class NameAttribute : Attribute
{
    public NameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

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

    [Name("Name")]
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("proOverall")]
    public int OverallRating { get; set; }
    
    [Name("Avg")]
    [JsonProperty("ratingAve")]
    public float AverageRating { get; set; }

    [Name("Apps")]
    [JsonProperty("gamesPlayed")]
    public int Appearances { get; set; }

    [Name("Goals")]
    [JsonProperty("goals")]
    public int Goals { get; set; }

    [Name("Assists")]
    [JsonProperty("assists")]
    public int Assists { get; set; }

    [Name("Involvements")]
    public int GoalInvolvements => Goals + Assists;

    [Name("Shot (%)")]
    [JsonProperty("shotSuccessRate")]
    public int ShotSuccessRate { get; set; }

    [Name("Passes")]
    [JsonProperty("passesMade")]
    public int Passes { get; set; }

    [Name("Pass (%)")]
    [JsonProperty("passSuccessRate")]
    public int PassSuccessRate { get; set; }

    [Name("Tackles")]
    [JsonProperty("tacklesMade")]
    public int Tackles { get; set; }

    [Name("Tackle (%)")]
    [JsonProperty("tackleSuccessRate")]
    public int TackleSuccessRate { get; set; }

    [Name("Reds")]
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
