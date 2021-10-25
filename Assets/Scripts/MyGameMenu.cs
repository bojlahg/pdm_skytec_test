using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameMenu : MonoBehaviour, IUserInterface
{
    public Sprite m_PauseIconSprite;

    private GUIScreenMenu m_Menu;

    public void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIScreenMenu>("ScreenMenu", 0);
        m_Menu.onBackKeyDown = PauseButton_Click;

        GUIButton buttonPause = m_Menu.GetControl<GUIButton>("PauseButton");
        buttonPause.SetCaption(null);
        buttonPause.SetIcon(m_PauseIconSprite);
        buttonPause.onButtonClick = PauseButton_Click;
        buttonPause.Show();

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

    private void PauseButton_Click()
    {
        m_Menu.Hide();
        MyApp.instance.m_MyGame.PauseGame();
        MyApp.instance.m_MyPauseMenu.Create();
    }
}
