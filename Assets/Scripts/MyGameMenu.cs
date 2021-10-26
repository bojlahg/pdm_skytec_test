using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameMenu : MonoBehaviour, IUserInterface
{
    public Sprite m_PauseIconSprite;

    private GUIScreenMenu m_Menu;
    private GUIText m_TimerText;

    private void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIScreenMenu>("ScreenMenu", 0);
        m_Menu.onBackKeyDown = PauseButton_Click;

        m_TimerText = m_Menu.GetControl<GUIText>("TimerText");
        m_TimerText.SetText("0");
        m_TimerText.Show();

        GUIButton buttonPause = m_Menu.GetControl<GUIButton>("PauseButton");
        buttonPause.SetCaption(null);
        buttonPause.SetIcon(m_PauseIconSprite);
        buttonPause.onButtonClick = PauseButton_Click;
        buttonPause.Show();

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

    private void PauseButton_Click()
    {
        m_Menu.Hide();
        MyApp.instance.m_MyGame.PauseGame();
        MyApp.instance.m_MyPauseMenu.Show();
    }

    public void SetTimer(float t)
    {
        //System.TimeSpan ts = new System.TimeSpan(0, 0, (int)t);
        //m_TimerText.SetText(string.Format("{0}", ts));

        int mins = (int)(t / 60.0f);
        int secs = (int)(t - mins * 60);

        if (mins > 0)
        {
            m_TimerText.SetText(string.Format("{0:00}:{1:00}", mins, secs));
        }
        else
        {
            m_TimerText.SetText(string.Format("{0}", secs));
        }
    }
}
