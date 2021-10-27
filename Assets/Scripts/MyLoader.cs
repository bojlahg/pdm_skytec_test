using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLoader : MonoBehaviour, IGUILogic
{
    public string[] m_ProgressTexts;

    private GUILoader m_Loader;
    private GUIProgressBar m_ProgressBar;
    private int m_PrevTextIndex = -1;

    public GUIPanel GetPanel()
    {
        if (m_Loader == null)
        {
            m_PrevTextIndex = -1;

            m_Loader = GUIManager.instance.Create<GUILoader>(this, "Loader", 2);
            m_Loader.SetTitle("Загрузка");
            m_Loader.onAppearFinish = LoaderStarted;
            m_Loader.onDisappearFinish = GUIManager.instance.Destroy;

            m_ProgressBar = m_Loader.Create<GUIProgressBar>("ProgressBar");
            m_ProgressBar.SetProgress(0);
            m_ProgressBar.SetText("Загружаем ресурсы...");
            m_ProgressBar.Show();
        }
        return m_Loader;
    }

    private void Free()
    {
        GUIManager.instance.Destroy(m_Loader);
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

    private void LoaderStarted(GUIControl ctl)
    {
        StartCoroutine(LoadingProgressSimulation());
    }

    public IEnumerator LoadingProgressSimulation()
    {
        float timer = 0, duration = 3.0f;

        while (timer < duration)
        {
            Refresh(timer / duration);
            yield return null;
            timer += Time.deltaTime;
        }

        GUIManager.instance.ReplaceTop(MyApp.instance.m_MyMainMenu);
    }
}
