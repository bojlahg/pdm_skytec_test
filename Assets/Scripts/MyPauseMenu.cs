using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPauseMenu : MonoBehaviour, IUserInterface
{
    private GUIWindowMenu m_Menu;

    private void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIWindowMenu>("WindowMenu", 0);
        m_Menu.SetTitle("Пауза");
        m_Menu.onDisappearFinish = GUIManager.instance.Destroy;
        m_Menu.onBackKeyDown = HomeButton_Click;

        GUIButton buttonSettings = m_Menu.Create<GUIButton>("Button");
        buttonSettings.SetCaption("Настройки");
        buttonSettings.SetIcon(null);
        buttonSettings.onButtonClick = SettingsButton_Click;
        buttonSettings.Show();

        GUIButton buttonHome = m_Menu.Create<GUIButton>("Button");
        buttonHome.SetCaption("Выйти");
        buttonHome.SetIcon(null);
        buttonHome.onButtonClick = HomeButton_Click;
        buttonHome.Show();

        GUIButton buttonResume = m_Menu.Create<GUIButton>("Button");
        buttonResume.SetCaption("Продолжить");
        buttonResume.SetIcon(null);
        buttonResume.onButtonClick = ResumeButton_Click;
        buttonResume.Show();

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

    private void SettingsButton_Click()
    {
        m_Menu.Hide();
        MyApp.instance.m_MySettingsMenu.Show();
        MyApp.instance.m_MySettingsMenu.m_ReturnTo = this;
    }

    private void HomeButton_Click()
    {
        MyApp.instance.m_MyYesNoDialog.Show();
        MyApp.instance.m_MyYesNoDialog.SetTitle("Покинуть игру?");
        MyApp.instance.m_MyYesNoDialog.SetMessage("Вы точно хотите прервать неоконченную игру?");
        MyApp.instance.m_MyYesNoDialog.onAnswer = AbortDialogAnswer;
    }

    private void AbortDialogAnswer(int aidx)
    {
        if (aidx == 0)
        {
            m_Menu.Hide();
            MyApp.instance.m_MyMainMenu.Show();
            MyApp.instance.m_MyGame.AbortGame();
        }
    }

    private void ResumeButton_Click()
    {
        Hide();
        MyApp.instance.m_MyGameMenu.Show();
        MyApp.instance.m_MyGame.UnpauseGame();
    }
}
