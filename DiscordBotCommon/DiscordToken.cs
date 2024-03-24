using Discord;

namespace DiscordBotCommon
{
    public struct DiscordToken
    {
        public DiscordToken(string token, TokenType tokenType = TokenType.Bot)
        {
            Token = token;
            TokenType = tokenType;
        }

        public string Token { get; private set; }
        public TokenType TokenType { get; private set; }
    }
}
