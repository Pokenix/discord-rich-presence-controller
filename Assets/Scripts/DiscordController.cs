using Discord;
using UnityEngine;

public class DiscordController : MonoBehaviour
{
    // Discord Rich Presence Controller
    // Made by Pokenix
    // https://www.pokenix.com/discord-rich-presence-controller.html
    // https://www.discord.gg/STcThtu

    public Discord.Discord discord;
    public bool connected;

    public void Connect(long id)
    {
        if (connected)
        {
            discord.Dispose();
            connected = false;
        }
        discord = new Discord.Discord(id, (ulong)CreateFlags.Default);
        connected = true;
    }

    public void Disconnect()
    {
        if (connected)
        {
            discord.Dispose();
        }
    }

    public void UpdateActivity(Activity activity)
    {
        if (connected)
        {
            var activityManager = discord.GetActivityManager();
            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res != Result.Ok)
                {
                    Debug.LogWarning("[Discord] Couldn't update activity!");
                }
            });
        }
    }

    private void Update()
    {
        if (connected)
        {
            discord.RunCallbacks();
        }
    }

    private void OnApplicationQuit()
    {
        Disconnect();
    }
}
