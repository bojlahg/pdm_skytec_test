using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyResultsMenu : MonoBehaviour, IGUILogic
{
    public string[] m_ResultStrings;
    public Color[] m_ResultColors;
    public int m_Result = 0, m_ScoreAnimFrom = 0, m_ScoreAnimTo = 0;

    private GUIWindowMenu m_Menu;
    private GUIText m_ScoreText;
    private bool m_Animation = false;
    private Settings.Setting<string> m_UsernameSetting;

    private void Start()
    {
        m_UsernameSetting = Settings.instance.GetSetting<string>("Username");
    }

    public GUIPanel GetPanel()
    {
        if (m_Menu == null)
        {
            m_Menu = GUIManager.instance.Create<GUIWindowMenu>(this, "WindowMenu", 0);
            m_Menu.SetTitle("Результаты");
            m_Menu.onAppearFinish = AppearFinish;
            m_Menu.onDisappearFinish = GUIManager.instance.Destroy;
            m_Menu.onBackKeyDown = OkButton_Click;

            GUIText resultText = m_Menu.Create<GUIText>("Text");
            resultText.SetText(m_ResultStrings[m_Result]);
            resultText.SetColor(m_ResultColors[m_Result]);
            resultText.Show();

            GUIText usernameText = m_Menu.Create<GUIText>("Text");
            usernameText.SetText(string.Format("Игрок: {0}", m_UsernameSetting.value));
            usernameText.Show();

            m_ScoreText = m_Menu.Create<GUIText>("Text");
            m_ScoreText.SetText(string.Format("Количество очков: {0}", m_ScoreAnimFrom));
            m_ScoreText.Show();

            GUIButton okButton = m_Menu.Create<GUIButton>("Button");
            okButton.SetCaption("Назад");
            okButton.SetIcon(null);
            okButton.onButtonClick = OkButton_Click;
            okButton.Show();
        }
        return m_Menu;
    } 

    private void AppearFinish(GUIControl ctl)
    {
        StartCoroutine(AnimScore());
    }

    private IEnumerator AnimScore()
    {
        m_Animation = true;
        int score = m_ScoreAnimFrom;
        int delta = 1;
        if(m_ScoreAnimTo - m_ScoreAnimFrom < 0)
        {
            delta = -1;
        }
        SoundManager.instance.PlayLooped("Score");
        while (score != m_ScoreAnimTo)
        {
            yield return null;
            m_ScoreText.SetText(string.Format("Количество очков: {0}", score));
            score += delta;
        }
        SoundManager.instance.StopLooped("Score");
        m_ScoreText.SetText(string.Format("Количество очков: {0}", m_ScoreAnimTo));
        m_Animation = false;
    }

    private void OkButton_Click()
    {
        if (!m_Animation)
        {
            SoundManager.instance.PlayOnce("ButtonClick");

            GUIManager.instance.ReplaceTop(MyApp.instance.m_MyMainMenu);
        }
    }
}
