using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMainMenu : MonoBehaviour
{
    private GUIMenu m_Menu;

    public void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIMenu>(0);
        m_Menu.SetTitle("Главное меню");
        m_Menu.onDisappearFinish = GUIManager.instance.Destroy;
        m_Menu.onBackKeyDown = BackButton_Click;

        GUIText winsText = m_Menu.Create<GUIText>();
        winsText.SetText(string.Format("Количество побед: {0}\nКоличество поражений: {1}", MyApp.instance.m_MySettings.m_WinCount, MyApp.instance.m_MySettings.m_LossCount));
        winsText.Show();

        GUIButton buttonPlay = m_Menu.Create<GUIButton>();
        buttonPlay.SetCaption("Играть");
        buttonPlay.SetIcon(null);
        buttonPlay.onButtonClick = PlayButton_Click;
        buttonPlay.Show();

        GUIButton buttonSettings = m_Menu.Create<GUIButton>();
        buttonSettings.SetCaption("Настройки");
        buttonSettings.SetIcon(null);
        buttonSettings.onButtonClick = SettingsButton_Click;
        buttonSettings.Show();

        GUIButton buttonCredits = m_Menu.Create<GUIButton>();
        buttonCredits.SetCaption("О Игре");
        buttonCredits.SetIcon(null);
        buttonCredits.onButtonClick = CreditsButton_Click;
        buttonCredits.Show();

        GUIButton backButton = m_Menu.Create<GUIButton>();
        backButton.SetCaption("Назад");
        backButton.SetIcon(null);
        backButton.onButtonClick = BackButton_Click;
        backButton.Show();

        m_Menu.Show();
    }

    public void Free()
    {
        m_Menu.Hide();
    }

    private void PlayButton_Click()
    {
        MyApp.instance.m_MyModeMenu.Create();
        m_Menu.Hide();
    }

    private void SettingsButton_Click()
    {
        MyApp.instance.m_MySettingsMenu.Create();
        m_Menu.Hide();
    }

    private void CreditsButton_Click()
    {
        MyApp.instance.m_MyCreditsMenu.Create();
        m_Menu.Hide();
    }

    private void BackButton_Click()
    {
        MyApp.instance.m_MyExitDialog.Create();
    }
}
