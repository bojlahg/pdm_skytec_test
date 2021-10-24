using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPauseMenu : MonoBehaviour
{
    private GUIWindowMenu m_Menu;

    public void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIWindowMenu>(0);
        m_Menu.SetTitle("Пауза");
        m_Menu.onDisappearFinish = GUIManager.instance.Destroy;
        m_Menu.onBackKeyDown = HomeButton_Click;

        GUIButton buttonSettings = m_Menu.Create<GUIButton>();
        buttonSettings.SetCaption("Настройки");
        buttonSettings.SetIcon(null);
        buttonSettings.onButtonClick = SettingsButton_Click;
        buttonSettings.Show();

        GUIButton buttonHome = m_Menu.Create<GUIButton>();
        buttonHome.SetCaption("Выйти");
        buttonHome.SetIcon(null);
        buttonHome.onButtonClick = HomeButton_Click;
        buttonHome.Show();

        GUIButton buttonResume = m_Menu.Create<GUIButton>();
        buttonResume.SetCaption("Продолжить");
        buttonResume.SetIcon(null);
        buttonResume.onButtonClick = ResumeButton_Click;
        buttonResume.Show();

        m_Menu.Show();
    }

    private void SettingsButton_Click()
    {
        m_Menu.Hide();
        MyApp.instance.m_MySettingsMenu.Create();
    }

    private void HomeButton_Click()
    {
    }

    private void ResumeButton_Click()
    {
    }
}
