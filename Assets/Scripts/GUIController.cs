using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    public GameObject m_LoaderPrefab, m_MenuPrefab, m_DialogPrefab;
    public Transform m_LoaderLayer, m_MenuLayer, m_DialogLayer;

    public static GUIController instance { get { return m_Instance; } }

    private static GUIController m_Instance;

    private void Awake()
    {
        m_Instance = this;

        m_LoaderPrefab.SetActive(false);
        m_MenuPrefab.SetActive(false);
        m_DialogPrefab.SetActive(false);
        
    }

    public GUILoader CreateLoader()
    {
        GameObject go = GameObject.Instantiate(m_LoaderPrefab, m_LoaderLayer);
        return go.GetComponent<GUILoader>();
    }

    public GUIMenu CreateMenu()
    {
        GameObject go = GameObject.Instantiate(m_MenuPrefab, m_MenuLayer);
        return go.GetComponent<GUIMenu>();
    }

    public GUIDialog CreateDialog()
    {
        GameObject go = GameObject.Instantiate(m_DialogPrefab, m_DialogLayer);
        return go.GetComponent<GUIDialog>();
    }

    public void Destroy(GUITemplate tmp)
    {
        GameObject.Destroy(tmp.gameObject);
    }
}
