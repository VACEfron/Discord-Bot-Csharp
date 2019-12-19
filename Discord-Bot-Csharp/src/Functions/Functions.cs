﻿using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Discord_Bot
{
    public static class Functions
    {
        public static async Task SetBotStatus(DiscordSocketClient Client)
        {
            using (StreamReader configjson = new StreamReader(Directory.GetCurrentDirectory().Replace(@"\bin\Debug", "") + @"\src\Config.json")) // Get the config file. Note: this gets the current directory and removes the '\bin\Debug' part and replaces it with 'Config.json'. Your directory may differ.
            {
                var readJSON = configjson.ReadToEnd(); // Read the Config.json file.
                var config = (JObject)JsonConvert.DeserializeObject(readJSON); // Deserialize the JSON.

                // Get all needed JSON objects.
                string currently = config["currently"].Value<string>(); 
                string playingstatus = config["playing_status"].Value<string>();
                string status = config["status"].Value<string>();

                //The following code will check the JSON file and change the bot status accordingly.

                if (currently.ToLower() == "playing")
                {
                    await Client.SetGameAsync(playingstatus, "https://www.google.com/", ActivityType.Playing);
                    Console.WriteLine($"Playing status set | Playing: {playingstatus}");
                }
                if (currently.ToLower() == "listening")
                {
                    await Client.SetGameAsync(playingstatus, "https://www.google.com/", ActivityType.Listening);
                    Console.WriteLine($"Playing status set | Listening: {playingstatus}");
                }
                if (currently.ToLower() == "watching")
                {
                    await Client.SetGameAsync(playingstatus, "https://www.google.com/", ActivityType.Watching);
                    Console.WriteLine($"Playing status set | Watching: {playingstatus}");
                }
                if (currently.ToLower() == "streaming")
                {
                    await Client.SetGameAsync(playingstatus, "https://www.google.com/", ActivityType.Streaming);
                    Console.WriteLine($"Playing status set | Streaming: {playingstatus}");
                }
                if (status.ToLower() == "online")
                {
                    await Client.SetStatusAsync(UserStatus.Online);
                    Console.WriteLine($"Online status set | Online");
                }
                if (status.ToLower() == "dnd")
                {
                    await Client.SetStatusAsync(UserStatus.DoNotDisturb);
                    Console.WriteLine($"Online status set | Do Not Disturb");
                }
                if (status.ToLower() == "idle")
                {
                    await Client.SetStatusAsync(UserStatus.Idle);
                    Console.WriteLine($"Online status set | Idle");
                }
                if (status.ToLower() == "offline")
                {
                    await Client.SetStatusAsync(UserStatus.Invisible);
                    Console.WriteLine($"Online status set | Offline");
                }
            }
        }

        public static string GetAvatarUrl(SocketUser user)
        {
            return user.GetAvatarUrl()?.Replace("size=128", "size=1024") ?? user.GetDefaultAvatarUrl().Replace("size=128", "size=1024"); 
            // Get user avatar and resize it 1024x1024. If the user has no avatar, get the default Discord logo.
        }
    }
}