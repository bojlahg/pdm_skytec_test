using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMainMenu : MonoBehaviour, IGUILogic
{
    private GUIWindowMenu m_Menu;
    private Settings.Setting<string> m_UsernameSetting;
    private Settings.Setting<int> m_ScoreCountSetting;

    private void Start()
    {
        m_UsernameSetting = Settings.instance.GetSetting<string>("Username");
        m_ScoreCountSetting = Settings.instance.GetSetting<int>("ScoreCount");
    }

    public GUIPanel GetPanel()
    {
        if(m_Menu == null)
        {
            m_Menu = GUIManager.instance.Create<GUIWindowMenu>(this, "WindowMenu", 0);
            m_Menu.SetTitle("Главное меню");
            m_Menu.onDisappearFinish = GUIManager.instance.Destroy;
            m_Menu.onBackKeyDown = BackButton_Click;

            GUIText usernameText = m_Menu.Create<GUIText>("Text");
            usernameText.SetText(string.Format("Игрок: {0}", m_UsernameSetting.value));
            usernameText.Show();

            GUIText scoreText = m_Menu.Create<GUIText>("Text");
            scoreText.SetText(string.Format("Количество очков: {0}", m_ScoreCountSetting.value));
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
        }
        return m_Menu;
    }

    private void PlayButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        GUIManager.instance.ReplaceTop(MyApp.instance.m_MyModeMenu);
    }

    private void SettingsButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        GUIManager.instance.ReplaceTop(MyApp.instance.m_MySettingsMenu);

        MyApp.instance.m_MySettingsMenu.m_ReturnTo = this;
    }

    private void CreditsButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        GUIManager.instance.ReplaceTop(MyApp.instance.m_MyCreditsMenu);
    }

    private void BackButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        GUIManager.instance.AddTop(MyApp.instance.m_MyYesNoDialog);
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
