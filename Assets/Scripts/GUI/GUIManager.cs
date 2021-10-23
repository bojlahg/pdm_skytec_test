using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public GameObject[] m_Prefabs, m_DeactObjects;
    public Transform[] m_Layers;

    public static GUIManager instance { get { return m_Instance; } }

    private static GUIManager m_Instance;

    private Dictionary<string, GameObject> m_PrefabDict = new Dictionary<string, GameObject>();
    private List<GUIPanel> m_PanelStack = new List<GUIPanel>();


    private void Awake()
    {
        m_Instance = this;

        for(int i = 0; i < m_Prefabs.Length; ++i)
        {
            GUIControl ctl = m_Prefabs[i].GetComponent<GUIControl>();
            if (ctl != null)
            {
                if (!m_PrefabDict.ContainsKey(ctl.GetType().Name))
                {
                    m_PrefabDict.Add(ctl.GetType().Name, m_Prefabs[i]);
                }
                else
                {
                    Debug.LogErrorFormat("GUIManager: Duplicate prefab type name: {0}", ctl.GetType().Name);
                }
            }
            m_Prefabs[i].SetActive(false);
        }

        for (int i = 0; i < m_DeactObjects.Length; ++i)
        {
            m_DeactObjects[i].SetActive(false);
        }
    }

    public T Create<T>(int layerIdx) where T: GUIControl
    {
        string typename = typeof(T).Name;
        GameObject prefab;
        if(m_PrefabDict.TryGetValue(typename, out prefab))
        {
            T obj = GameObject.Instantiate(prefab, m_Layers[layerIdx]).GetComponent<T>();
            obj.Init();
            return obj;
        }
        Debug.LogErrorFormat("Unknown control type name: {0}", typename);
        return null;
    }

    public T Create<T>(Transform tfm) where T : GUIControl
    {
        string typename = typeof(T).Name;
        GameObject prefab;
        if (m_PrefabDict.TryGetValue(typename, out prefab))
        {
            T obj = GameObject.Instantiate(prefab, tfm).GetComponent<T>();
            obj.Init();
            return obj;
        }
        Debug.LogErrorFormat("Unknown control type name: {0}", typename);
        return null;
    }

    public void Destroy(GUIControl ctl)
    {
        GameObject.Destroy(ctl.gameObject);
    }

    public void Push(GUIPanel ctl)
    {
        Debug.LogFormat("Push {0}", ctl.name);
        GUIPanel top = GetTopPanel();
        if(top != null)
        {
            top.LostFocus();
        }
        m_PanelStack.Add(ctl);
    }

    public void Pop()
    {
        Debug.LogFormat("Pop {0}", m_PanelStack[m_PanelStack.Count - 1].name);

        m_PanelStack.RemoveAt(m_PanelStack.Count - 1);

        GUIPanel top = GetTopPanel();
        if (top != null)
        {
            top.GotFocus();
        }
    }

    public GUIPanel GetTopPanel()
    {
        if(m_PanelStack.Count > 0)
        { 
            return m_PanelStack[m_PanelStack.Count - 1];
        }
        return null;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GUIPanel ctl = GetTopPanel();
            if (ctl != null)
            {
                if (ctl.onBackKeyDown != null)
                {
                    ctl.onBackKeyDown.Invoke();
                }
            }
        }
    }
}
