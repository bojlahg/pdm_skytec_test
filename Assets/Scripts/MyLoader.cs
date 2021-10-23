using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLoader : MonoBehaviour
{
    public string[] m_ProgressTexts;
    public MyMainMenu m_MyMainMenu;

    private GUILoader m_Loader;
    private GUIProgressBar m_ProgressBar;
    private int m_PrevTextIndex = -1;

    public void Create()
    {
        m_PrevTextIndex = -1;

        m_Loader = GUIManager.instance.Create<GUILoader>(2);
        m_Loader.SetTitle("Загрузка");
        m_Loader.onDisappearFinish = LoaderFinished;

        m_ProgressBar = m_Loader.Create<GUIProgressBar>();
        m_ProgressBar.SetProgress(0);
        m_ProgressBar.SetText("Загружаем ресурсы...");
        m_ProgressBar.Show();

        m_Loader.Show();
    }

    public void Free()
    {
        m_Loader.Hide();
    }

    public void Refresh(float v)
    {
        int textIndex = Mathf.FloorToInt(v * m_ProgressTexts.Length);
        if (textIndex != m_PrevTextIndex && textIndex < m_ProgressTexts.Length)
        {
            m_PrevTextIndex = textIndex;
            m_ProgressBar.SetText(m_ProgressTexts[textIndex]);
        }
        m_ProgressBar.SetProgress(v);
    }

    private void LoaderFinished(GUIControl ctl)
    {
        m_MyMainMenu.Create();
    }
}
