using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMainMenu : MonoBehaviour
{
    public MySettings m_MySettings;
    public MyModeMenu m_MyModeMenu;
    public MySettingsMenu m_MySettingsMenu;
    public MyCreditsMenu m_MyCreditsMenu;
    public MyExitDialog m_MyExitDialog;

    private GUIMenu m_MainMenu;

    public void Create()
    {
        m_MainMenu = GUIManager.instance.Create<GUIMenu>(0);
        m_MainMenu.SetTitle("Главное меню");
        m_MainMenu.onDisappearFinish = GUIManager.instance.Destroy;
        m_MainMenu.onBackKeyDown = BackButton_Click;

        GUIText winsText = m_MainMenu.Create<GUIText>();
        winsText.SetText(string.Format("Количество побед: {0}\nКоличество поражений: {1}", m_MySettings.m_WinCount, m_MySettings.m_LossCount));
        winsText.Show();

        GUIButton buttonPlay = m_MainMenu.Create<GUIButton>();
        buttonPlay.SetCaption("Играть");
        buttonPlay.SetIcon(null);
        buttonPlay.onButtonClick = PlayButton_Click;
        buttonPlay.Show();

        GUIButton buttonSettings = m_MainMenu.Create<GUIButton>();
        buttonSettings.SetCaption("Настройки");
        buttonSettings.SetIcon(null);
        buttonSettings.onButtonClick = SettingsButton_Click;
        buttonSettings.Show();

        GUIButton buttonCredits = m_MainMenu.Create<GUIButton>();
        buttonCredits.SetCaption("О Игре");
        buttonCredits.SetIcon(null);
        buttonCredits.onButtonClick = CreditsButton_Click;
        buttonCredits.Show();

        GUIButton backButton = m_MainMenu.Create<GUIButton>();
        backButton.SetCaption("Назад");
        backButton.SetIcon(null);
        backButton.onButtonClick = BackButton_Click;
        backButton.Show();

        m_MainMenu.Show();
    }

    public void Free()
    {
        m_MainMenu.Hide();
    }

    private void PlayButton_Click()
    {
        m_MyModeMenu.Create();
        m_MainMenu.Hide();
    }

    private void SettingsButton_Click()
    {
        m_MySettingsMenu.Create();
        m_MainMenu.Hide();
    }

    private void CreditsButton_Click()
    {
        m_MyCreditsMenu.Create();
        m_MainMenu.Hide();
    }

    private void BackButton_Click()
    {
        m_MyExitDialog.Create();
    }
}
