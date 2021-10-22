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
    private List<GUIContainer> m_ContainerStack = new List<GUIContainer>();


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

    public void Push(GUIContainer ctl)
    {
        GUIContainer top = GetTopContainer();
        if(top != null)
        {
            top.LostFocus();
        }
        m_ContainerStack.Add(ctl);
    }

    public void Pop()
    {
        m_ContainerStack.RemoveAt(m_ContainerStack.Count - 1);

        GUIContainer top = GetTopContainer();
        if (top != null)
        {
            top.GotFocus();
        }
    }

    public GUIContainer GetTopContainer()
    {
        if(m_ContainerStack.Count > 0)
        { 
            return m_ContainerStack[m_ContainerStack.Count - 1];
        }
        return null;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GUIContainer ctl = GetTopContainer();
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
