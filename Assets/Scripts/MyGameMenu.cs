using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameMenu : MonoBehaviour, IGUILogic
{
    public Sprite m_PauseIconSprite;

    private GUIScreenMenu m_Menu;
    private GUIText m_TimerText;

    public GUIPanel GetPanel()
    {
        if(m_Menu == null)
        {
            m_Menu = GUIManager.instance.Create<GUIScreenMenu>(this, "ScreenMenu", 0);
            m_Menu.onBackKeyDown = PauseButton_Click;

            m_TimerText = m_Menu.GetControl<GUIText>("TimerText");
            m_TimerText.SetText("0");
            m_TimerText.Show();

            GUIButton buttonPause = m_Menu.GetControl<GUIButton>("PauseButton");
            buttonPause.SetCaption(null);
            buttonPause.SetIcon(m_PauseIconSprite);
            buttonPause.onButtonClick = PauseButton_Click;
            buttonPause.Show();
        }
        return m_Menu;
    }

    private void PauseButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        GUIManager.instance.ReplaceTop(MyApp.instance.m_MyPauseMenu);
        MyApp.instance.m_MyGame.PauseGame();
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
