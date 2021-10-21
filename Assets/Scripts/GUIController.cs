using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    public GameObject m_LoaderPrefab, m_MenuPrefab, m_DialogPrefab;
    public Transform m_PrefabRoot, m_LoaderLayer, m_MenuLayer, m_DialogLayer;

    public static GUIController instance { get { return m_Instance; } }

    private static GUIController m_Instance;

    private void Awake()
    {
        m_Instance = this;

        InitRecursive(m_PrefabRoot);
    }

    public void InitRecursive(Transform root)
    {
        for (int i = 0; i < root.childCount; ++i)
        {
            Transform child = root.GetChild(i);
            GUIControl ctl = child.GetComponent<GUIControl>();
            if (ctl != null)
            {
                ctl.gameObject.SetActive(false);
            }
            InitRecursive(child);
        }
    }

    public GUILoader CreateLoader()
    {
        GameObject go = GameObject.Instantiate(m_LoaderPrefab, m_LoaderLayer);
        GUILoader loader = go.GetComponent<GUILoader>();
        loader.Init();
        return loader;
    }

    public GUIMenu CreateMenu()
    {
        GameObject go = GameObject.Instantiate(m_MenuPrefab, m_MenuLayer);
        GUIMenu menu = go.GetComponent<GUIMenu>();
        menu.Init();
        return menu;
    }

    public GUIDialog CreateDialog()
    {
        GameObject go = GameObject.Instantiate(m_DialogPrefab, m_DialogLayer);
        GUIDialog dialog = go.GetComponent<GUIDialog>();
        dialog.Init();
        return dialog;
    }

    public void Destroy(GUIControl ctl)
    {
        GameObject.Destroy(ctl.gameObject);
    }
}
