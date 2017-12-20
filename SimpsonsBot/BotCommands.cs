using System;
using RestSharp;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace SimpsonsBot
{
    public class BotCommands
    {
        /**
         * Basic Hello command, bot will reply "👋 Hi, (Discord User)!".
         */
        [Command("hi")]
        [Description("The bot will say hello to you.")]
        public async Task Test(CommandContext ctx)
        {
            Console.WriteLine(ctx.User.Username + $": hi; local -> {DateTime.Now.ToString("hh:mm:ss yyyy-MM-dd")}");
            await ctx.RespondAsync($"🖳 Hi, Super Nintendo {ctx.User.Mention}!");
        }


        /**
         * Will retrieve a random simpsons quote from: "http://www.randomsimpsonsquote.com/".
         */
        string quoteHtml = string.Empty;
        [Command("randomquote")]
        [Description("The bot will search and display a random quote from The Simpsons.")]
        public async Task RandomQuote(CommandContext ctx)
        {

            var client = new RestClient("http://www.randomsimpsonsquote.com/");
            var request = new RestRequest("", Method.GET);

            client.ExecuteAsync(request, response =>
            {
                quoteHtml = response.Content;
            });

            //String formating (removes unneceary HTML text).
            quoteHtml = quoteHtml.Substring(quoteHtml.IndexOf("<blockquote>") + 12);
            quoteHtml = "*" + quoteHtml.Substring(0, quoteHtml.IndexOf("</blockquote>"));
            quoteHtml = quoteHtml.Replace(":", ":*");
            quoteHtml = quoteHtml.Replace("<br /><br /> ", "\n*");
            
            string randomQuote = quoteHtml;

            Console.WriteLine(ctx.User.Username + $": randomquote; GET-request={client.BaseUrl} -> {DateTime.Now.ToString("hh:mm:ss yyyy-MM-dd")}");
            await ctx.RespondAsync($"Your Random Simpsons Quote:\n\n{randomQuote}");
        }


        /**
         * Will retrieve a random simpsons image from: "https://frinkiac.com/".
         */
        string frinkiacHtml = string.Empty;
        [Command("frinkiac")]
        [Description("The bot will search and display a random quote from The Simpsons.")]
        public async Task Frinkiac(CommandContext ctx)
        {       
            var client = new RestClient("https://frinkiac.com/");
            var request = new RestRequest("", Method.GET);

            client.ExecuteAsync(request, response =>
            {
                frinkiacHtml = response.Content;
            });

            frinkiacHtml = frinkiacHtml.Substring(frinkiacHtml.IndexOf("\"https://frinkiac.com/img/") + 1);
            string imgUrl = frinkiacHtml.Substring(0, frinkiacHtml.IndexOf("\""));

            //Console.WriteLine(imgUrl);
            Console.WriteLine(ctx.User.Username + $": frinkiac; GET-request={client.BaseUrl} -> {DateTime.Now.ToString("hh:mm:ss yyyy-MM-dd")}");
            await ctx.RespondAsync($"Random Frinkiac:\n{imgUrl}");          
        }
    }
}
