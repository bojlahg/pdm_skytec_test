using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCreditsMenu : MonoBehaviour, IGUILogic
{
    private GUIWindowMenu m_Menu;

    public GUIPanel GetPanel()
    {
        if (m_Menu == null)
        {
            m_Menu = GUIManager.instance.Create<GUIWindowMenu>(this, "WindowMenu", 0);
            m_Menu.SetTitle("О Игре");
            m_Menu.onDisappearFinish = GUIManager.instance.Destroy;
            m_Menu.onBackKeyDown = BackButton_Click;

            GUIText creditsText = m_Menu.Create<GUIText>("Text");
            creditsText.SetText("Разработчик:\nДмитрий Приходько\nbojlahg@gmail.com");
            creditsText.Show();

            GUIButton backButton = m_Menu.Create<GUIButton>("Button");
            backButton.SetCaption("Назад");
            backButton.SetIcon(null);
            backButton.onButtonClick = BackButton_Click;
            backButton.Show();
        }
        return m_Menu;
    }

    private void BackButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        GUIManager.instance.ReplaceTop(MyApp.instance.m_MyMainMenu);
    }
}
