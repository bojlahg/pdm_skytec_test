using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySettingsMenu : MonoBehaviour
{
    private GUIMenu m_Menu;

    public void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIMenu>(0);
        m_Menu.SetTitle("Настройки");
        m_Menu.onDisappearFinish = GUIManager.instance.Destroy;
        m_Menu.onBackKeyDown = BackButton_Click;

        GUISlider volumeSlider = m_Menu.Create<GUISlider>();
        volumeSlider.SetCaption("Громкость");
        volumeSlider.SetValue(MyApp.instance.m_MySettings.m_Volume);
        volumeSlider.onValueChanged = VolumeSlider_ValueChanged;
        volumeSlider.Show();

        GUIToggle musicToggle = m_Menu.Create<GUIToggle>();
        musicToggle.SetCaption("Музыка");
        musicToggle.SetCheckState(MyApp.instance.m_MySettings.m_MusicEnabled);
        musicToggle.onValueChanged = MusicToggle_ValueChanged;
        musicToggle.Show();

        GUIButton backButton = m_Menu.Create<GUIButton>();
        backButton.SetCaption("Назад");
        backButton.SetIcon(null);
        backButton.onButtonClick = BackButton_Click;
        backButton.Show();

        m_Menu.Show();
    }

    private void VolumeSlider_ValueChanged(GUIControl ctl, float v)
    {
        MyApp.instance.m_MySettings.m_Volume = v;
    }

    private void MusicToggle_ValueChanged(GUIControl ctl, bool c)
    {
        MyApp.instance.m_MySettings.m_MusicEnabled = c;
    }

    private void BackButton_Click()
    {
        MyApp.instance.m_MySettings.StoreData();
        MyApp.instance.m_MyMainMenu.Create();
        m_Menu.Hide();
    }
}
