﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMainMenu : MonoBehaviour, IUserInterface
{
    private GUIWindowMenu m_Menu;

    public void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIWindowMenu>("WindowMenu", 0);
        m_Menu.SetTitle("Главное меню");
        m_Menu.onDisappearFinish = GUIManager.instance.Destroy;
        m_Menu.onBackKeyDown = BackButton_Click;

        GUIText winsText = m_Menu.Create<GUIText>("Text");
        winsText.SetText(string.Format("Количество побед: {0}\nКоличество поражений: {1}", MyApp.instance.m_MySettings.m_WinCount, MyApp.instance.m_MySettings.m_LossCount));
        winsText.Show();

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

    public void Free()
    {
        GUIManager.instance.Destroy(m_Menu);
    }

    public void Show()
    {
        m_Menu.Show();
    }

    public void Hide()
    {
        m_Menu.Hide();
    }

    private void PlayButton_Click()
    {
        m_Menu.Hide();
        MyApp.instance.m_MyModeMenu.Create();
    }

    private void SettingsButton_Click()
    {
        m_Menu.Hide();
        MyApp.instance.m_MySettingsMenu.Create();
        MyApp.instance.m_MySettingsMenu.m_ReturnTo = this;
    }

    private void CreditsButton_Click()
    {
        m_Menu.Hide();
        MyApp.instance.m_MyCreditsMenu.Create();        
    }

    private void BackButton_Click()
    {
        MyApp.instance.m_MyYesNoDialog.Create();
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
