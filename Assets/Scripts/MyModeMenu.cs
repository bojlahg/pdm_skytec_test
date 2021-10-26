using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyModeMenu : MonoBehaviour, IUserInterface
{
    private GUIWindowMenu m_Menu;

    private Settings.Setting<string> m_UsernameSetting;
    private Settings.Setting<int> m_GameModeIndexSetting;

    private void Start()
    {
        m_UsernameSetting = Settings.instance.GetSetting<string>("Username");
        m_GameModeIndexSetting = Settings.instance.GetSetting<int>("GameModeIndex");
    }

    private void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIWindowMenu>("WindowMenu", 0);
        m_Menu.SetTitle("Режим игры");
        m_Menu.onDisappearFinish = GUIManager.instance.Destroy;
        m_Menu.onBackKeyDown = BackButton_Click;

        GUIInputField userInput = m_Menu.Create<GUIInputField>("InputField");
        userInput.SetCaption("Ваше имя");
        userInput.SetText(m_UsernameSetting.value);
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
        rgrp.SetRadioIndex(m_GameModeIndexSetting.value);
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

    private void UsernameInput_ValueChanged(GUIControl ctl, string v)
    {
        m_UsernameSetting.value = v;
    }

    private void GameModeRadioGroup_ValueChanged(GUIControl ctl, int v)
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        m_GameModeIndexSetting.value = v;
    }

    private void PlayButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        MyApp.instance.m_Settings.Commit();
        m_Menu.Hide();
        MyApp.instance.m_MyGameMenu.Show();
        MyApp.instance.m_MyGame.StartGame();
    }

    private void BackButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        MyApp.instance.m_Settings.Commit();
        m_Menu.Hide();
        MyApp.instance.m_MyMainMenu.Show();
    }
}
