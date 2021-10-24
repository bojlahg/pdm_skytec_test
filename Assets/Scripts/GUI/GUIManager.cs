using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public Transform m_PrefabRoot;
    public Transform[] m_Layers;

    public static GUIManager instance { get { return m_Instance; } }

    private static GUIManager m_Instance;

    private Dictionary<string, GameObject> m_PrefabDict = new Dictionary<string, GameObject>();
    private List<GUIPanel> m_PanelStack = new List<GUIPanel>();


    private void Awake()
    {
        m_Instance = this;

        SearchPrefabsRecursive(m_PrefabRoot);

        m_PrefabRoot.gameObject.SetActive(false);
    }

    public void SearchPrefabsRecursive(Transform parent)
    {
        for(int i = 0; i < parent.childCount; ++i)
        {
            Transform child = parent.GetChild(i);

            GUIControl ctl = child.GetComponent<GUIControl>();
            if (ctl != null)
            {
                if (!m_PrefabDict.ContainsKey(ctl.gameObject.name))
                {
                    m_PrefabDict.Add(ctl.gameObject.name, ctl.gameObject);
                }
                else
                {
                    Debug.LogErrorFormat("GUIManager: Duplicate prefab name: {0}", ctl.GetType().Name);
                }
            }
            else
            {
                SearchPrefabsRecursive(child);
            }
        }
    }

    public T Create<T>(string n, int layerIdx) where T: GUIControl
    {
        GameObject prefab;
        if(m_PrefabDict.TryGetValue(n, out prefab))
        {
            GUIControl ctl = GameObject.Instantiate(prefab, m_Layers[layerIdx]).GetComponent<T>();
            if(ctl != null && ctl is T)
            {
                ctl.Init();
                return (T)ctl;
            }
            else
            {
                Debug.LogErrorFormat("Control type mismach: {0} instead of {1}", ctl.GetType().Name, typeof(T).Name);
            }
        }
        Debug.LogErrorFormat("Unknown control name: {0}", n);
        return null;
    }

    public T Create<T>(string n, Transform tfm) where T : GUIControl
    {
        GameObject prefab;
        if (m_PrefabDict.TryGetValue(n, out prefab))
        {
            GUIControl ctl = GameObject.Instantiate(prefab, tfm).GetComponent<T>();
            if (ctl != null && ctl is T)
            {
                ctl.Init();
                return (T)ctl;
            }
            else
            {
                Debug.LogErrorFormat("Control type mismach: {0} instead of {1}", ctl.GetType().Name, typeof(T).Name);
            }
        }
        Debug.LogErrorFormat("Unknown control name: {0}", n);
        return null;
    }

    public void Destroy(GUIControl ctl)
    {
        GameObject.Destroy(ctl.gameObject);
    }

    public void Push(GUIPanel ctl)
    {
        GUIPanel top = GetTopPanel();
        if(top != null)
        {
            top.LostFocus();
        }
        m_PanelStack.Add(ctl);
    }

    public void Pop()
    {
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
