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
        loader.SetTitle("��������");
        loader.SetProgressText("��������� �������...");
        loader.SetProgress(0);
        loader.onHideFinish += InitialLoaderFinished;
        loader.Show();
        yield return new WaitForSeconds(1);
        loader.SetProgressText("��������� ��������...");
        loader.SetProgress(0.25f);
        yield return new WaitForSeconds(1);
        loader.SetProgressText("��������� �����...");
        loader.SetProgress(0.5f);
        yield return new WaitForSeconds(1);
        loader.SetProgressText("��������� �������...");
        loader.SetProgress(0.75f);
        yield return new WaitForSeconds(1);
        loader.SetProgressText("��������� ��������...");
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
