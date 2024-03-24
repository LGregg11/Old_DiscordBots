using ApiClient;
using Newtonsoft.Json;
using ProClubsBot.Models;
using System.Net.Http.Headers;

namespace ProClubsBot
{
    internal class ProClubsApiClient
    {
        public const string DefaultClubId = "16075195";
        public const string BaseAddress = "https://proclubs.ea.com/api/fc/";
        public const string Host = "proclubs.ea.com";
        public const string UserAgentName = "Chrome";
        public const string UserAgentVersion = "122.0.0.0";

        public const string Platform = "common-gen5";

        private readonly IApiClient client;

        public ProClubsApiClient(IApiClient client)
        {
            this.client = client;

            client.SetBaseAddress(BaseAddress);
            client.SetHost(Host);
            client.SetTimeout(TimeSpan.FromSeconds(5));
            client.SetUserAgent(new ProductInfoHeaderValue(UserAgentName, UserAgentVersion));
            client.SetAcceptRequestHeader(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Squad? GetSquadStats(string platform = Platform, string clubId = DefaultClubId)
        {
            string endpoint = $"members/stats?platform={platform}&clubId={clubId}";

            return JsonConvert.DeserializeObject<Squad>(GetStringResponse(endpoint));
        }

        public Team? GetTeamStats(string platform = Platform, string clubId = DefaultClubId)
        {
            string endpoint = $"clubs/overallStats?platform={platform}&clubIds={clubId}";

            return JsonConvert.DeserializeObject<List<Team>>(GetStringResponse(endpoint))?.FirstOrDefault();
        }

        public string? GetTeamNameFromClubId(string platform = Platform, string clubId = DefaultClubId)
        {
            string endpoint = $"clubs/info?platform={platform}&clubIds={clubId}";

            if (JsonConvert.DeserializeObject<Dictionary<string, TeamInfo>>(GetStringResponse(endpoint)) is not Dictionary<string, TeamInfo> teamInfo)
                return null;

            return teamInfo[clubId].Name;
        }

        private string GetStringResponse(string endpoint)
        {
            HttpResponseMessage response = client.Get(endpoint);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode.ToString());
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
