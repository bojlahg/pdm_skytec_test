using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySettingsMenu : MonoBehaviour
{
    public MySettings m_MySettings;
    public MyMainMenu m_MyMainMenu;

    private GUIMenu m_SettingsMenu;

    public void Create()
    {
        m_SettingsMenu = GUIManager.instance.CreateMenu();
        m_SettingsMenu.SetTitle("Настройки");
        m_SettingsMenu.onDisappearFinish = GUIManager.instance.Destroy;
        m_SettingsMenu.onBackKeyDown = BackButton_Click;

        GUISlider volumeSlider = m_SettingsMenu.CreateSlider();
        volumeSlider.SetCaption("Громкость");
        volumeSlider.SetValue(m_MySettings.m_Volume);
        volumeSlider.onValueChanged = VolumeSlider_ValueChanged;
        volumeSlider.Show();

        GUIToggle musicToggle = m_SettingsMenu.CreateToggle();
        musicToggle.SetCaption("Музыка");
        musicToggle.SetCheckState(m_MySettings.m_MusicEnabled);
        musicToggle.onValueChanged = MusicToggle_ValueChanged;
        musicToggle.Show();

        GUIButton backButton = m_SettingsMenu.CreateButton();
        backButton.SetCaption("Назад");
        backButton.SetIcon(null);
        backButton.onButtonClick = BackButton_Click;
        backButton.Show();

        m_SettingsMenu.Show();
    }

    private void VolumeSlider_ValueChanged(GUIControl ctl, float v)
    {
        m_MySettings.m_Volume = v;
    }

    private void MusicToggle_ValueChanged(GUIControl ctl, bool c)
    {
        m_MySettings.m_MusicEnabled = c;
    }

    private void BackButton_Click()
    {
        m_MySettings.StoreData();
        m_MyMainMenu.Create();
        m_SettingsMenu.Hide();
    }
}
