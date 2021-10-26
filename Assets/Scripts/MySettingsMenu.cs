using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySettingsMenu : MonoBehaviour, IUserInterface
{
    private GUIWindowMenu m_Menu;

    public IUserInterface m_ReturnTo;

    private void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIWindowMenu>("WindowMenu", 0);
        m_Menu.SetTitle("Настройки");
        m_Menu.onDisappearFinish = GUIManager.instance.Destroy;
        m_Menu.onBackKeyDown = BackButton_Click;

        GUISlider volumeSlider = m_Menu.Create<GUISlider>("Slider");
        volumeSlider.SetCaption("Громкость");
        volumeSlider.SetValue(MyApp.instance.m_Settings.m_SoundVolume);
        volumeSlider.onValueChanged = VolumeSlider_ValueChanged;
        volumeSlider.Show();

        GUIToggle musicToggle = m_Menu.Create<GUIToggle>("Toggle");
        musicToggle.SetCaption("Музыка");
        musicToggle.SetCheckState(MyApp.instance.m_Settings.m_MusicEnabled);
        musicToggle.onValueChanged = MusicToggle_ValueChanged;
        musicToggle.Show();

        GUIButton backButton = m_Menu.Create<GUIButton>("Button");
        backButton.SetCaption("Назад");
        backButton.SetIcon(null);
        backButton.onButtonClick = BackButton_Click;
        backButton.Show();

        m_Menu.Show();
    }

    private void Free()
    {
        GUIManager.instance.Destroy(m_Menu);
    }

    public void Show()
    {
        Create();
        m_Menu.Show();
    }

    public void Hide()
    {
        m_Menu.Hide();
    }

    private void VolumeSlider_ValueChanged(GUIControl ctl, float v)
    {
        MyApp.instance.m_Settings.m_SoundVolume = v;
    }

    private void MusicToggle_ValueChanged(GUIControl ctl, bool c)
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        MyApp.instance.m_Settings.m_MusicEnabled = c;
    }

    private void BackButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        MyApp.instance.m_Settings.StoreData();
        m_Menu.Hide();
        m_ReturnTo.Show();
    }
}
