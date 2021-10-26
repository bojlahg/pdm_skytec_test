using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyApp : MonoBehaviour
{
    public static MyApp instance { get { return m_Instance; } }

    private static MyApp m_Instance;

    public Settings m_Settings;
    public MyGame m_MyGame;
    public MyLoader m_MyLoader;
    public MyMainMenu m_MyMainMenu;
    public MyModeMenu m_MyModeMenu;
    public MySettingsMenu m_MySettingsMenu;
    public MyCreditsMenu m_MyCreditsMenu;
    public MyYesNoDialog m_MyYesNoDialog;
    public MyGameMenu m_MyGameMenu;
    public MyPauseMenu m_MyPauseMenu;
    public MyResultsMenu m_MyResultsMenu;

    private void Awake()
    {
        m_Instance = this;

        m_Settings.AddSetting<float>("SoundVolume", 1);
        m_Settings.AddSetting<bool>("MusicEnabled", true);
        m_Settings.AddSetting<int>("ScoreCount", 0);
        m_Settings.AddSetting<int>("GameModeIndex", 0);
        m_Settings.AddSetting<string>("Username", "Username");

        m_Settings.RestoreData();
    }

    private void Start()
    {
        m_MyLoader.Show();
    }
}
