using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyApp : MonoBehaviour
{
    
    public MyLoader m_MyLoader;


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
