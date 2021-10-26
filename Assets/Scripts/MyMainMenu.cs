using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMainMenu : MonoBehaviour, IUserInterface
{
    private GUIWindowMenu m_Menu;

    private void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIWindowMenu>("WindowMenu", 0);
        m_Menu.SetTitle("Главное меню");
        m_Menu.onDisappearFinish = GUIManager.instance.Destroy;
        m_Menu.onBackKeyDown = BackButton_Click;

        GUIText usernameText = m_Menu.Create<GUIText>("Text");
        usernameText.SetText(string.Format("Игрок: {0}", MyApp.instance.m_Settings.m_Username));
        usernameText.Show();

        GUIText scoreText = m_Menu.Create<GUIText>("Text");
        scoreText.SetText(string.Format("Количество очков: {0}", MyApp.instance.m_Settings.m_ScoreCount));
        scoreText.Show();

        GUIButton buttonPlay = m_Menu.Create<GUIButton>("Button");
        buttonPlay.SetCaption("Играть");
        buttonPlay.SetIcon(null);
        buttonPlay.onButtonClick = PlayButton_Click;
        buttonPlay.Show();

        GUIButton buttonSettings = m_Menu.Create<GUIButton>("Button");
        buttonSettings.SetCaption("Настройки");
        buttonSettings.SetIcon(null);
        buttonSettings.onButtonClick = SettingsButton_Click;
        buttonSettings.Show();

        GUIButton buttonCredits = m_Menu.Create<GUIButton>("Button");
        buttonCredits.SetCaption("О Игре");
        buttonCredits.SetIcon(null);
        buttonCredits.onButtonClick = CreditsButton_Click;
        buttonCredits.Show();

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

    private void PlayButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        m_Menu.Hide();
        MyApp.instance.m_MyModeMenu.Show();
    }

    private void SettingsButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        m_Menu.Hide();
        MyApp.instance.m_MySettingsMenu.Show();
        MyApp.instance.m_MySettingsMenu.m_ReturnTo = this;
    }

    private void CreditsButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        m_Menu.Hide();
        MyApp.instance.m_MyCreditsMenu.Show();        
    }

    private void BackButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        MyApp.instance.m_MyYesNoDialog.Show();
        MyApp.instance.m_MyYesNoDialog.SetTitle("Ой-Ой!");
        MyApp.instance.m_MyYesNoDialog.SetMessage("Вы точно хотите выйти из игры?");
        MyApp.instance.m_MyYesNoDialog.onAnswer = QuitDialogAnswer;
    }

    private void QuitDialogAnswer(int aidx)
    {
        if(aidx == 0)
        {
            Application.Quit();
        }
        else
        {

        }
    }
}
