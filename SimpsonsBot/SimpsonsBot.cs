using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace SimpsonsBot
{
    class SimpsonsBot
    {
        static DiscordClient discord;
        public static CommandsNextModule commands;

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            //Initialize the Discord client.
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = "MzkyMjEzMjkwMzgzODM1MTQ2.DRj81Q.3_fHKcw9pko-WCiqiJuGWroggOA",
                TokenType = TokenType.Bot,

                //Output debug information about the discord bot.
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
        });


            //Initialize the CommandsNextModule for this bot.
            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                //Set the bots command prefix to "$".
                StringPrefix = "$"
            });

            //Get/Enable commands from custom Command Module (i.e BotCommands.cs).
            commands.RegisterCommands<BotCommands>();

            await discord.ConnectAsync();
            //Keep the console application running.
            await Task.Delay(-1);
        }
    }
}
