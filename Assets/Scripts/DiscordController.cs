using Discord;
using UnityEngine;
using UnityEngine.UI;

public class DiscordController : MonoBehaviour
{
    // Discord Rich Presence Controller
    // Made by Pokenix
    // https://www.pokenix.com/discord-rich-presence-controller
    // https://www.discord.gg/STcThtu

    public Manager manager;

    public Discord.Discord discord;
    public bool connected;

    public void Connect(long id)
    {
        if (!connected)
        {
            discord = new Discord.Discord(id, (ulong)CreateFlags.NoRequireDiscord);
            connected = true;
            manager.button_connect.GetComponent<Button>().interactable = false;
        }
    }

    public void Disconnect()
    {
        if (connected)
        {
            discord.Dispose();
        }
    }

    public void Clear()
    {
        if (connected)
        {
            var activityManager = discord.GetActivityManager();
            activityManager.ClearActivity((res) =>
            {
                if (res != Result.Ok)
                {
                    Debug.LogWarning("[Discord] Couldn't clear activity!");
                }
            });
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
