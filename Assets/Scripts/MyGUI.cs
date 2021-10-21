using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGUI : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadingProgressSimulation());
    }

    private IEnumerator LoadingProgressSimulation()
    {
        GUILoader loader = GUIController.instance.CreateLoader();
        loader.SetTitle("Загрузка");
        loader.SetProgressText("Загружаем ресурсы...");
        loader.SetProgress(0);
        loader.onHideFinish += InitialLoaderFinished;
        loader.Show();
        yield return new WaitForSeconds(1);
        loader.SetProgressText("Загружаем картинки...");
        loader.SetProgress(0.25f);
        yield return new WaitForSeconds(1);
        loader.SetProgressText("Загружаем звуки...");
        loader.SetProgress(0.5f);
        yield return new WaitForSeconds(1);
        loader.SetProgressText("Загружаем скрипты...");
        loader.SetProgress(0.75f);
        yield return new WaitForSeconds(1);
        loader.SetProgressText("Загружаем анимации...");
        loader.SetProgress(1.0f);
        yield return new WaitForSeconds(1);
        loader.Hide();
        GUIController.instance.Destroy(loader);
    }

    private void InitialLoaderFinished()
    {
        GUIMenu menu = GUIController.instance.CreateMenu();
        menu.Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
