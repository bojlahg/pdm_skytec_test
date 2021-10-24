﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyModeMenu : MonoBehaviour, IUserInterface
{
    private GUIWindowMenu m_Menu;

    public void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIWindowMenu>("WindowMenu", 0);
        m_Menu.SetTitle("Режим игры");
        m_Menu.onDisappearFinish = GUIManager.instance.Destroy;
        m_Menu.onBackKeyDown = BackButton_Click;

        GUIInputField userInput = m_Menu.Create<GUIInputField>("InputField");
        userInput.SetCaption("Ваше имя");
        userInput.SetText(MyApp.instance.m_MySettings.m_Username);
        userInput.SetHintText("Введите имя...");
        userInput.onValueChanged = UsernameInput_ValueChanged;
        userInput.Show();

        GUIRadioGroup rgrp = m_Menu.Create<GUIRadioGroup>("RadioGroup");
        rgrp.onValueChanged = GameModeRadioGroup_ValueChanged;
        rgrp.SetCaption(null);
        GUIRadio[] rdos = rgrp.CreateMultiple<GUIRadio>("Radio", 4);
        
        for (int i = 0; i < rdos.Length; ++i)
        {
            rdos[i].SetCaption(string.Format("Поле {0}x{0}", 3 + i));
            //rdos[i].SetCheckState(i == MyApp.instance.m_MySettings.m_GameModeIndex);
            rdos[i].Show();
        }
        rgrp.SetRadioIndex(MyApp.instance.m_MySettings.m_GameModeIndex);
        rgrp.Show();

        GUIButton playButton = m_Menu.Create<GUIButton>("Button");
        playButton.SetCaption("Играть");
        playButton.SetIcon(null);
        playButton.onButtonClick = PlayButton_Click;
        playButton.Show();

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

    private void UsernameInput_ValueChanged(GUIControl ctl, string v)
    {
        MyApp.instance.m_MySettings.m_Username = v;
    }

    private void GameModeRadioGroup_ValueChanged(GUIControl ctl, int v)
    {
        MyApp.instance.m_MySettings.m_GameModeIndex = v;
    }

    private void PlayButton_Click()
    {
        MyApp.instance.m_MySettings.StoreData();
        m_Menu.Hide();
        MyApp.instance.m_MyGameMenu.Create();
        MyApp.instance.m_MyGame.StartGame(MyApp.instance.m_MySettings.m_GameModeIndex);
    }

    private void BackButton_Click()
    {
        MyApp.instance.m_MySettings.StoreData();
        m_Menu.Hide();
        MyApp.instance.m_MyMainMenu.Create();
    }
}
