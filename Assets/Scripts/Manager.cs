using Discord;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    // Discord Rich Presence Controller
    // Made by Pokenix
    // https://www.pokenix.com/discord-rich-presence-controller
    // https://www.discord.gg/STcThtu

    public DiscordController discordController;

    public GameObject info;
    public GameObject main;
    public GameObject button_connect;
    public GameObject menu;
    public GameObject menu_basic;
    public GameObject menu_images;
    public GameObject menu_buttons;

    public GameObject id;
    public GameObject details;
    public GameObject state;
    public GameObject timeStart;
    public GameObject timeEnd;
    public GameObject instance;
    public GameObject largeKey;
    public GameObject largeText;
    public GameObject smallKey;
    public GameObject smallText;

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void OpenInfo()
    {
        info.SetActive(true);
        main.SetActive(false);
    }

    public void CloseInfo()
    {
        info.SetActive(false);
        main.SetActive(true);
    }

    public void ChangeMenu()
    {
        int value = menu.GetComponent<TMP_Dropdown>().value;
        switch (value)
        {
            case 0:
                menu_basic.SetActive(true);
                menu_images.SetActive(false);
                menu_buttons.SetActive(false);
                break;
            case 1:
                menu_basic.SetActive(false);
                menu_images.SetActive(true);
                menu_buttons.SetActive(false);
                break;
            case 2:
                menu_basic.SetActive(false);
                menu_images.SetActive(false);
                menu_buttons.SetActive(true);
                break;
        }
    }

    public void Connect()
    {
        if (id.GetComponent<TMP_InputField>().text.Length > 0)
        {
            discordController.Connect(long.Parse(id.GetComponent<TMP_InputField>().text));
        }
    }

    public void Clear()
    {
        discordController.Clear();
    }

    public void UpdateActivity()
    {
        var activity = new Activity { };
        if (details.GetComponent<TMP_InputField>().text.Length > 0)
        {
            activity.Details = details.GetComponent<TMP_InputField>().text;
        }
        if (state.GetComponent<TMP_InputField>().text.Length > 0)
        {
            activity.State = state.GetComponent<TMP_InputField>().text;
        }
        if (timeStart.GetComponent<TMP_InputField>().text.Length > 0)
        {
            if (timeStart.GetComponent<TMP_InputField>().text.ToLower() == "now")
            {
                activity.Timestamps.Start = DateTimeOffset.Now.ToUnixTimeSeconds();
            }
            else
            {
                activity.Timestamps.Start = long.Parse(timeStart.GetComponent<TMP_InputField>().text);
            }
        }
        if (timeEnd.GetComponent<TMP_InputField>().text.Length > 0)
        {
            activity.Timestamps.End = long.Parse(timeEnd.GetComponent<TMP_InputField>().text);
        }
        activity.Instance = instance.GetComponent<Toggle>().isOn;
        if (largeKey.GetComponent<TMP_InputField>().text.Length > 0)
        {
            activity.Assets.LargeImage = largeKey.GetComponent<TMP_InputField>().text;
        }
        if (largeText.GetComponent<TMP_InputField>().text.Length > 0)
        {
            activity.Assets.LargeText = largeText.GetComponent<TMP_InputField>().text;
        }
        if (smallKey.GetComponent<TMP_InputField>().text.Length > 0)
        {
            activity.Assets.SmallImage = smallKey.GetComponent<TMP_InputField>().text;
        }
        if (smallText.GetComponent<TMP_InputField>().text.Length > 0)
        {
            activity.Assets.SmallText = smallText.GetComponent<TMP_InputField>().text;
        }
        discordController.UpdateActivity(activity);
    }
}
