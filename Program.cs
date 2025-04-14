

using Discord;
using Discord.WebSocket;

namespace PetMineBot;

class Program
{
    // SETTINGS
    private static string token = "";
    private static ulong channelId = 0;
    // SETTINGS


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
            (currentTime.Hour == 5 && currentTime.Minute == 40) ||
            (currentTime.Hour == 11 && currentTime.Minute == 40) ||
            (currentTime.Hour == 17 && currentTime.Minute == 40) ||
            (currentTime.Hour == 23 && currentTime.Minute == 40)
            )
            {
                if (client != null)
                {
                    // Change to correct channel id
                    var channel = client.GetChannel(channelId) as IMessageChannel;
                    if (channel != null)
                    {
                        // Send the message
                        var message = await channel.SendMessageAsync("@everyone The next raid starts in 20 minutes -- react with 👍 if you're joining!");
                        var emoji = new Emoji("👍");
                        await message.AddReactionAsync(emoji);
                        Console.WriteLine("Message Sent");
                        await Task.Delay(80000);
                    }
                }

            }
            await Task.Delay(5000);
        }
    }
}