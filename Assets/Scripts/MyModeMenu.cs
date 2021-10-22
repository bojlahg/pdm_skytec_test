using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyModeMenu : MonoBehaviour
{
    private GUIMenu m_Menu;

    public void Create()
    {
        m_Menu = GUIManager.instance.Create<GUIMenu>(0);
        GUIButton[] btns = m_Menu.CreateMultiple<GUIButton>(5);
        foreach(GUIButton btn in btns)
        {
            btn.SetCaption("Test");
            btn.SetIcon(null);
            btn.Show();
        }
        m_Menu.Show();
    }
}
