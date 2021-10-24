using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIContainer : GUIControl
{
    public Transform m_ControlRoot;

    private Dictionary<string, GUIControl> m_InitialControls = new Dictionary<string, GUIControl>();

    public override void Init()
    {
        base.Init();
        InitControlsRecursive(transform);
    }

    private void InitControlsRecursive(Transform parent)
    {
        for(int i = 0; i < parent.childCount; ++i)
        {
            Transform child = parent.GetChild(i);
            GUIControl ctl = child.GetComponent<GUIControl>();
            if(ctl != null)
            {
                ctl.Init();
                if(!m_InitialControls.ContainsKey(ctl.gameObject.name))
                {
                    m_InitialControls.Add(ctl.gameObject.name, ctl);
                }
                else
                {
                    Debug.LogErrorFormat("Duplicate initial control name: {0}", ctl.gameObject.name);
                }
            }
            InitControlsRecursive(child);
        }
    }

    public T GetControl<T>(string n) where T: GUIControl
    {
        GUIControl ctl;
        if(m_InitialControls.TryGetValue(n, out ctl))
        {
            if(ctl is T)
            {
                return (T)ctl;
            }
            else
            {
                Debug.LogErrorFormat("Initial control is of another type: {0} instead of {1}", ctl.GetType().Name, typeof(T).Name);
            }
        }
        else
        {
            Debug.LogErrorFormat("Can't find initial control with name: {0}", ctl.gameObject.name);
        }
        return null;
    }

    public virtual void OnCreateControl(GUIControl ctl)
    {
    }

    public virtual void OnDestroyControl(GUIControl ctl)
    {
    }

    public T Create<T>(string n) where T : GUIControl
    {
        T ctl = GUIManager.instance.Create<T>(n, m_ControlRoot);
        OnCreateControl(ctl);
        return ctl;
    }

    public T[] CreateMultiple<T>(string n, int cnt) where T : GUIControl
    {
        T[] ctls = new T[cnt];
        for (int i = 0; i < cnt; ++i)
        {
            ctls[i] = Create<T>(n);
        }
        return ctls;
    }

    public void Destroy(GUIControl ctl)
    {
        OnDestroyControl(ctl);
        GameObject.Destroy(ctl.gameObject);
    }
}
