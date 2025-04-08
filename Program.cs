

using Discord;
using Discord.WebSocket;

namespace PetMineBot;

class Program
{
    private static string token = "";
    private static DiscordSocketClient? client;
    public static async Task Main()
    {
        Console.WriteLine("Starting bot");

        client = new DiscordSocketClient();

        client.Ready += ClientReadyHandler;

        await client.LoginAsync(Discord.TokenType.Bot, token);
        await client.StartAsync();

        await Task.Delay(-1);
    }

    static async Task ClientReadyHandler()
    {
        System.Console.WriteLine("running");
        while (true)
        {
            DateTime currentTime = DateTime.Now;
            if (
            (currentTime.Hour == 5 && currentTime.Minute == 55) ||
            (currentTime.Hour == 11 && currentTime.Minute == 55) ||
            (currentTime.Hour == 17 && currentTime.Minute == 55) ||
            (currentTime.Hour == 23 && currentTime.Minute == 55)
            )
            {
                if (client != null)
                {
                    // Change to correct channel id
                    var channel = client.GetChannel(1358521192579072130) as IMessageChannel;
                    if (channel != null)
                    {
                        await channel.SendMessageAsync("@everyone Clan Wars in 5 minutes");
                        Console.WriteLine("Message Sent");
                        await Task.Delay(80000);
                    }
                }

            }
            await Task.Delay(1000);
        }
    }
}