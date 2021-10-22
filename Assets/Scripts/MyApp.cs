using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyApp : MonoBehaviour
{
    public static MyApp instance { get { return m_Instance; } }

    private static MyApp m_Instance;

    public MySettings m_MySettings;
    public MyLoader m_MyLoader;
    public MyMainMenu m_MyMainMenu;
    public MyModeMenu m_MyModeMenu;
    public MySettingsMenu m_MySettingsMenu;
    public MyCreditsMenu m_MyCreditsMenu;
    public MyExitDialog m_MyExitDialog;

    private void Awake()
    {
        m_Instance = this;
    }

    private void Start()
    {
        m_MyLoader.Create();
        StartCoroutine(LoadingProgressSimulation());
    }

    private IEnumerator LoadingProgressSimulation()
    {
        float timer = 0, duration = 3.0f;
        
        while (timer < duration)
        {
            m_MyLoader.Refresh(timer / duration);
            yield return null;
            timer += Time.deltaTime;
        }
        m_MyLoader.Free();
    }
}
