﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySettingsMenu : MonoBehaviour, IUserInterface
{
    public IUserInterface m_ReturnTo;

    private GUIWindowMenu m_Menu;
    private Settings.Setting<float> m_SoundVolumeSetting;
    private Settings.Setting<bool> m_MusicEnabledSetting;

    private void Start()
    {
        m_SoundVolumeSetting = Settings.instance.GetSetting<float>("SoundVolume");
        m_MusicEnabledSetting = Settings.instance.GetSetting<bool>("MusicEnabled");
    }

    private void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIWindowMenu>("WindowMenu", 0);
        m_Menu.SetTitle("Настройки");
        m_Menu.onDisappearFinish = GUIManager.instance.Destroy;
        m_Menu.onBackKeyDown = BackButton_Click;

        GUISlider volumeSlider = m_Menu.Create<GUISlider>("Slider");
        volumeSlider.SetCaption("Громкость");
        volumeSlider.SetValue(m_SoundVolumeSetting.value);
        volumeSlider.onValueChanged = VolumeSlider_ValueChanged;
        volumeSlider.Show();

        GUIToggle musicToggle = m_Menu.Create<GUIToggle>("Toggle");
        musicToggle.SetCaption("Музыка");
        musicToggle.SetCheckState(m_MusicEnabledSetting.value);
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
        m_SoundVolumeSetting.value = v;
    }

    private void MusicToggle_ValueChanged(GUIControl ctl, bool c)
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        m_MusicEnabledSetting.value = c;
    }

    private void BackButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        MyApp.instance.m_Settings.Commit();
        m_Menu.Hide();
        m_ReturnTo.Show();
    }
}
