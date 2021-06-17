from Python.DiscordBotClient import DiscordBotClient
from Python.ProClubsBot.ProClubsWebScraper import ProClubsWebScraper
import pathlib
import json
import pandas as pd

CONFIGS = {
    "bot_name": "ProClubsBot",
    "file_path": pathlib.Path(__file__).parent.absolute(),
    "command_prefix": "-"
}


class ProClubsBot(DiscordBotClient):
    def __init__(self, team_name="AntiFun", command_prefix=None, debug=False):
        super().__init__(
            CONFIGS["bot_name"],
            CONFIGS["file_path"],
            CONFIGS["command_prefix"] if not command_prefix else command_prefix,
            debug
        )
        self.team_name = team_name
        self.discord_emotes |= {
            "veteran": ":older_adult:",
            "blood": ":drop_of_blood:",
            "inactive": ":sleeping:",
            "W": ":white_check_mark:",
            "L": ":x:",
            "N": ":shrug:"
        }
        self.commands = {
            "help": {
                "function": self.get_help,
                "help": f"{self.command_prefix}help"
            },
            "club": {
                "function": self.get_club,
                "help": f"{self.command_prefix}club \"team name\"*%s* not required (default to 'AntiFun')" % "\t"*15
            },
            "members": {
                "function": self.get_members,
                "help": f"{self.command_prefix}members \"team name\"*%s* not required (default to 'AntiFun')" % "\t"*15
            }
        }

        self.pro_clubs_web_scraper = ProClubsWebScraper()

    def _get_team_name(self, *args):
        if len(args) == 1:
            return args[0]
        return self.team_name

    async def on_message(self, ctx):
        if not ctx.content.startswith(self.command_prefix):
            return

        full_command = ctx.content.removeprefix(self.command_prefix)

        if " " in full_command:
            command = full_command.split(" ")[0]
            args = self.arg_split(" ".join(full_command.split(" ")[1:]))
        else:
            command = full_command
            args = []

        func = self.commands.get(command.lower(), {"function": self.invalid_command})["function"]
        await func(ctx.channel, *args)

    async def invalid_command(self, channel, *args):
        """
        The default command returned if the command requested is invalid
        :param channel: The discord channel that the command was sent from.
        """

        await channel.send(f"Invalid command\nType {self.command_prefix}help for list of commands!")

    async def get_help(self, channel):
        """
        The help command, listing all the commands available in the class (self.commands.keys())
        :param channel: The discord channel that the command was sent from.
        """

        join_str = f"\n\t"
        command_help_list = []
        for command in self.commands.keys():
            command_help_list.append(self.commands[command]["help"])

        await channel.send(f"```list of commands:{join_str}" + join_str.join(command_help_list) + "```")

    async def get_club(self, channel, *args):
        team_name = self._get_team_name(*args)
        response = self.pro_clubs_web_scraper.get_club(team_name)
        print(json.dumps(response, indent=4, sort_keys=True))
        await channel.send("check logs")

    async def get_members(self, channel, *args):
        team_name = self._get_team_name(*args)
        response = self.pro_clubs_web_scraper.get_members(team_name)

        """
        Goals avg,
        Assists avg,
        Goal inv.,
        Goal inv. avg,
        """
        columns = {
            "proName": "Name",
            "gamesPlayed": "Apps",
            "goals": "Goals",
            "assists": "Assists",
            "passesMade": "Passes",
            "passSuccessRate": "Pass %",
            "shotSuccessRate": "Shots/Goal %",
            "redCards": "Dan collectables (Reds)",
            "manOfTheMatch": "Da Boss awards (MOTM)",
            "tacklesMade": "Tackles",
            "tackleSuccessRate": "Tackle %"
        }

        table = pd.DataFrame().from_dict(response["members"])
        table_string = table[[
            "proName",
            "gamesPlayed",
            "goals",
            "assists",
            "passesMade",
            "passSuccessRate",
            "shotSuccessRate",
            "redCards",
            "manOfTheMatch",
            "tacklesMade",
            "tackleSuccessRate"
                  ]].rename(columns=columns).to_string(index=False)

        await channel.send(f"```{table_string}```")