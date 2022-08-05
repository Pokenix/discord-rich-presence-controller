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
    public GameObject menu_times;
    public GameObject menu_images;
    public GameObject menu_buttons;

    public GameObject profileID;

    public bool autoUpdateEnabled = false;
    public long autoUpdateNextTime;
    public long autoUpdateNext;

    public GameObject id;
    public GameObject details;
    public GameObject state;
    public GameObject instance;
    public GameObject autoUpdateTime;
    public GameObject autoUpdate;
    public GameObject timeStart;
    public GameObject timeEnd;
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
                menu_times.SetActive(false);
                menu_images.SetActive(false);
                menu_buttons.SetActive(false);
                break;
            case 1:
                menu_basic.SetActive(false);
                menu_times.SetActive(true);
                menu_images.SetActive(false);
                menu_buttons.SetActive(false);
                break;
            case 2:
                menu_basic.SetActive(false);
                menu_times.SetActive(false);
                menu_images.SetActive(true);
                menu_buttons.SetActive(false);
                break;
            case 3:
                menu_basic.SetActive(false);
                menu_times.SetActive(false);
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
            activity.Details = details.GetComponent<TMP_InputField>().text.Replace("{time}", DateTime.Now.ToString("HH:mm"));
        }
        if (state.GetComponent<TMP_InputField>().text.Length > 0)
        {
            activity.State = state.GetComponent<TMP_InputField>().text.Replace("{time}", DateTime.Now.ToString("HH:mm"));
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Quit();
        if (Input.GetKeyDown(KeyCode.F1)) SaveProfile();
        if (Input.GetKeyDown(KeyCode.F2)) LoadProfile();
        if (Input.GetKeyDown(KeyCode.F3))
        {
            id.GetComponent<TMP_InputField>().text = "";
            details.GetComponent<TMP_InputField>().text = "";
            state.GetComponent<TMP_InputField>().text = "";
            instance.GetComponent<Toggle>().isOn = false;
            autoUpdateTime.GetComponent<TMP_InputField>().text = "";
            autoUpdate.GetComponent<Toggle>().isOn = false;
            timeStart.GetComponent<TMP_InputField>().text = "";
            timeEnd.GetComponent<TMP_InputField>().text = "";
            largeKey.GetComponent<TMP_InputField>().text = "";
            largeText.GetComponent<TMP_InputField>().text = "";
            smallKey.GetComponent<TMP_InputField>().text = "";
            smallText.GetComponent<TMP_InputField>().text = "";
        }
        if (Input.GetKeyDown(KeyCode.F8)) OpenURL("https://youtu.be/dQw4w9WgXcQ");
        if (Input.GetKeyDown(KeyCode.F11)) ChangeScreenMode();
        if (Input.GetKeyDown(KeyCode.Home)) OpenURL("https://www.pokenix.com/discord-rich-presence-controller");
        if (autoUpdateEnabled)
        {
            long now = DateTimeOffset.Now.ToUnixTimeSeconds();
            long next = autoUpdateNext;
            if (now >= next)
            {
                UpdateActivity();
                autoUpdateNext = DateTimeOffset.Now.AddSeconds(autoUpdateNextTime).ToUnixTimeSeconds();
            }
        }
    }

    public void ToggleAutoUpdate()
    {
        if (autoUpdate.GetComponent<Toggle>().isOn)
        {
            long timeNext = 60;
            if (autoUpdateTime.GetComponent<TMP_InputField>().text.Length > 0)
            {
                timeNext = long.Parse(autoUpdateTime.GetComponent<TMP_InputField>().text);
            }
            autoUpdateNext = timeNext;
            autoUpdateNextTime = timeNext;
            autoUpdateEnabled = true;
        }
        else
        {
            autoUpdateEnabled = false;
            autoUpdateNext = 0;
        }
    }

    public void SaveProfile()
    {
        string profile;
        if (profileID.GetComponent<TMP_InputField>().text.Length > 0) profile = profileID.GetComponent<TMP_InputField>().text; else profile = "0";
        PlayerPrefs.SetString($"{profile}-id", id.GetComponent<TMP_InputField>().text);
        PlayerPrefs.SetString($"{profile}-details", details.GetComponent<TMP_InputField>().text);
        PlayerPrefs.SetString($"{profile}-state", state.GetComponent<TMP_InputField>().text);
        if (instance.GetComponent<Toggle>().isOn) PlayerPrefs.SetInt($"{profile}-instance", 1); else PlayerPrefs.SetInt($"{profile}-instance", 0);
        PlayerPrefs.SetString($"{profile}-autoUpdateTime", autoUpdateTime.GetComponent<TMP_InputField>().text);
        if (autoUpdate.GetComponent<Toggle>().isOn) PlayerPrefs.SetInt($"{profile}-autoUpdate", 1); else PlayerPrefs.SetInt($"{profile}-autoUpdate", 0);
        PlayerPrefs.SetString($"{profile}-timeStart", timeStart.GetComponent<TMP_InputField>().text);
        PlayerPrefs.SetString($"{profile}-timeEnd", timeEnd.GetComponent<TMP_InputField>().text);
        PlayerPrefs.SetString($"{profile}-largeKey", largeKey.GetComponent<TMP_InputField>().text);
        PlayerPrefs.SetString($"{profile}-largeText", largeText.GetComponent<TMP_InputField>().text);
        PlayerPrefs.SetString($"{profile}-smallKey", smallKey.GetComponent<TMP_InputField>().text);
        PlayerPrefs.SetString($"{profile}-smallText", smallText.GetComponent<TMP_InputField>().text);
        PlayerPrefs.Save();
    }

    public void LoadProfile()
    {
        string profile;
        if (profileID.GetComponent<TMP_InputField>().text.Length > 0) profile = profileID.GetComponent<TMP_InputField>().text; else profile = "0";
        id.GetComponent<TMP_InputField>().text = PlayerPrefs.GetString($"{profile}-id");
        details.GetComponent<TMP_InputField>().text = PlayerPrefs.GetString($"{profile}-details");
        state.GetComponent<TMP_InputField>().text = PlayerPrefs.GetString($"{profile}-state");
        if (PlayerPrefs.GetInt($"{profile}-instance") == 1) instance.GetComponent<Toggle>().isOn = true; else instance.GetComponent<Toggle>().isOn = false;
        autoUpdateTime.GetComponent<TMP_InputField>().text = PlayerPrefs.GetString($"{profile}-autoUpdateTime");
        if (PlayerPrefs.GetInt($"{profile}-autoUpdate") == 1) autoUpdate.GetComponent<Toggle>().isOn = true; else autoUpdate.GetComponent<Toggle>().isOn = false;
        timeStart.GetComponent<TMP_InputField>().text = PlayerPrefs.GetString($"{profile}-timeStart");
        timeEnd.GetComponent<TMP_InputField>().text = PlayerPrefs.GetString($"{profile}-timeEnd");
        largeKey.GetComponent<TMP_InputField>().text = PlayerPrefs.GetString($"{profile}-largeKey");
        largeText.GetComponent<TMP_InputField>().text = PlayerPrefs.GetString($"{profile}-largeText");
        smallKey.GetComponent<TMP_InputField>().text = PlayerPrefs.GetString($"{profile}-smallKey");
        smallText.GetComponent<TMP_InputField>().text = PlayerPrefs.GetString($"{profile}-smallText");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeScreenMode()
    {
        if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
