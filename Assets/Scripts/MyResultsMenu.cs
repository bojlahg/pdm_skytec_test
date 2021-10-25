using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyResultsMenu : MonoBehaviour, IUserInterface
{
    private GUIWindowMenu m_Menu;

    public void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIWindowMenu>("WindowMenu", 0);
        m_Menu.SetTitle("Результаты");
        m_Menu.onDisappearFinish = GUIManager.instance.Destroy;
        m_Menu.onBackKeyDown = OkButton_Click;

        GUIButton okButton = m_Menu.Create<GUIButton>("Button");
        okButton.SetCaption("Назад");
        okButton.SetIcon(null);
        okButton.onButtonClick = OkButton_Click;
        okButton.Show();

        m_Menu.Show();
    }

    public void Free()
    {
        
    }

    public void Hide()
    {
        
    }

    public void Show()
    {
        
    }

    private void OkButton_Click()
    {
        m_Menu.Hide();
        MyApp.instance.m_MyMainMenu.Create();
    }
}
