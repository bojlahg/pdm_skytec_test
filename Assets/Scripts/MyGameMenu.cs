using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameMenu : MonoBehaviour
{
    public Sprite m_PauseIconSprite;

    private GUIScreenMenu m_Menu;

    public void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIScreenMenu>(0);

        GUIButton buttonPause = m_Menu.GetControl<GUIButton>("PauseButton");
        buttonPause.SetCaption(null);
        buttonPause.SetIcon(m_PauseIconSprite);
        buttonPause.onButtonClick = PauseButton_Click;
        buttonPause.Show();

        m_Menu.Show();
    }

    private void PauseButton_Click()
    {
        m_Menu.Hide();
        MyApp.instance.m_MyGame.PauseGame();
        MyApp.instance.m_MyPauseMenu.Create();
    }
}
