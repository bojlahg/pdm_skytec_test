using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCreditsMenu : MonoBehaviour
{
    private GUIMenu m_Menu;

    public void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIMenu>(0);
        m_Menu.SetTitle("О Игре");
        m_Menu.onDisappearFinish = GUIManager.instance.Destroy;
        m_Menu.onBackKeyDown = BackButton_Click;

        GUIText creditsText = m_Menu.Create<GUIText>();
        creditsText.SetText("Разработчик:\nДмитрий Приходько\nbojlahg@gmail.com");
        creditsText.Show();

        GUIButton backButton = m_Menu.Create<GUIButton>();
        backButton.SetCaption("Назад");
        backButton.SetIcon(null);
        backButton.onButtonClick = BackButton_Click;
        backButton.Show();

        m_Menu.Show();
    }

    private void BackButton_Click()
    {
        MyApp.instance.m_MyMainMenu.Create();
        m_Menu.Hide();
    }
}
