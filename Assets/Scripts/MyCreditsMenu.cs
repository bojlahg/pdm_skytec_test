using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCreditsMenu : MonoBehaviour, IUserInterface
{
    private GUIWindowMenu m_Menu;

    public void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIWindowMenu>("WindowMenu", 0);
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

    private void BackButton_Click()
    {
        m_Menu.Hide();
        MyApp.instance.m_MyMainMenu.Create();
    }
}
