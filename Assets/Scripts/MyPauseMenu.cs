using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPauseMenu : MonoBehaviour, IGUILogic
{
    private GUIWindowMenu m_Menu;

    public GUIPanel GetPanel()
    {
        if (m_Menu == null)
        {
            m_Menu = GUIManager.instance.Create<GUIWindowMenu>(this, "WindowMenu", 0);
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
        }
        return m_Menu;
    }

    private void SettingsButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        GUIManager.instance.ReplaceTop(MyApp.instance.m_MySettingsMenu);
        MyApp.instance.m_MySettingsMenu.m_ReturnTo = this;
    }

    private void HomeButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        GUIManager.instance.AddTop(MyApp.instance.m_MyYesNoDialog);
        MyApp.instance.m_MyYesNoDialog.SetTitle("Покинуть игру?");
        MyApp.instance.m_MyYesNoDialog.SetMessage("Вы точно хотите прервать неоконченную игру?");
        MyApp.instance.m_MyYesNoDialog.onAnswer = AbortDialogAnswer;
    }

    private void AbortDialogAnswer(int aidx)
    {
        if (aidx == 0)
        {
            GUIManager.instance.ReplaceTop(MyApp.instance.m_MyMainMenu);

            MyApp.instance.m_MyGame.AbortGame();
        }
    }

    private void ResumeButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        GUIManager.instance.ReplaceTop(MyApp.instance.m_MyGameMenu);

        MyApp.instance.m_MyGame.UnpauseGame();
    }
}
