

using Discord;
using Discord.WebSocket;

namespace PetMineBot;

class Program
{
    // SETTINGS
    private static string token = "";
    private static ulong channelId = 0;
    private static ulong guildID = 0;
    private static bool isRunning = true;
    // SETTINGS


    private static DiscordSocketClient? client;
    public static async Task Main()
    {
        Console.WriteLine("Starting bot");

        client = new DiscordSocketClient();

        client.Ready += ClientReadyHandler;
        client.SlashCommandExecuted += SlashCommandHandler;

        await client.LoginAsync(Discord.TokenType.Bot, token);
        await client.StartAsync();

        await Task.Delay(-1);
    }

    private static async Task SlashCommandHandler(SocketSlashCommand command)
    {
        switch (command.CommandName)
        {
            case "bot_on":
                isRunning = true;
                await command.RespondAsync("Bot Enabled");
                break;

            case "bot_off":
                isRunning = false;
                await command.RespondAsync("Bot Disabled");
                break;

            default:
                System.Console.WriteLine("Invalid Command");
                await command.RespondAsync("Invalid Command");
            break;
        }
    }

    static async Task ClientReadyHandler()
    {
        // Get guild(server)
        SocketGuild guild = client.GetGuild(guildID);
        
        var commands = new List<SlashCommandBuilder>
        {
            new SlashCommandBuilder()
            .WithName("bot_on")
            .WithDescription("Turns the bot on"),

            new SlashCommandBuilder()
            .WithName("bot_off")
            .WithDescription("Turns the bot off")
            
        };
        // Register Commands
        foreach(var cmd in commands) {
            await guild.CreateApplicationCommandAsync(cmd.Build());
        }
        System.Console.WriteLine("Commands Registered");


        // Get channel to message
        var channel = await client.GetChannelAsync(channelId) as IMessageChannel;

        // Main Loop
        while (true)
        {
            while (isRunning)
            {
                if (DateTime.Now.Minute == 50)
                {
                    if (channel != null)
                    {
                        await channel.SendMessageAsync("@everyone Egg Hunt in 10 minutes");
                        await Task.Delay(80000);
                    }
                }
                await Task.Delay(5000);
            }
            await Task.Delay(5000);
        }
    }
}