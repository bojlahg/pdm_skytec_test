using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCreditsMenu : MonoBehaviour
{
    public MyMainMenu m_MyMainMenu;

    private GUIMenu m_CreditsMenu;

    public void Create()
    {
        m_CreditsMenu = GUIManager.instance.Create<GUIMenu>(0);
        m_CreditsMenu.SetTitle("О Игре");
        m_CreditsMenu.onDisappearFinish = GUIManager.instance.Destroy;
        m_CreditsMenu.onBackKeyDown = BackButton_Click;

        GUIText creditsText = m_CreditsMenu.Create<GUIText>();
        creditsText.SetText("Разработчик:\nДмитрий Приходько\nbojlahg@gmail.com");
        creditsText.Show();

        GUIButton backButton = m_CreditsMenu.Create<GUIButton>();
        backButton.SetCaption("Назад");
        backButton.SetIcon(null);
        backButton.onButtonClick = BackButton_Click;
        backButton.Show();

        m_CreditsMenu.Show();
    }

    private void BackButton_Click()
    {
        m_MyMainMenu.Create();
        m_CreditsMenu.Hide();
    }
}
