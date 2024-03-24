using Newtonsoft.Json;

namespace ProClubsBot.Models
{
    internal class Squad
    {
        public Squad(List<Player> players)
        {
            Players = players;
        }

        [JsonProperty("members")]
        public List<Player> Players { get; set; }

        public override string ToString()
        {
            string squad = "Players:\n";
            foreach (Player player in Players)
                squad += $"  {player}\n";

            return squad;
        }
    }
}
