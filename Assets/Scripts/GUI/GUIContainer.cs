using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIContainer : GUIControl
{
    public Transform m_ControlRoot;

    public virtual void OnCreateControl(GUIControl ctl)
    {
    }

    public virtual void OnDestroyControl(GUIControl ctl)
    {
    }

    public T Create<T>() where T : GUIControl
    {
        T ctl = GUIManager.instance.Create<T>(m_ControlRoot);
        OnCreateControl(ctl);
        return ctl;
    }

    public T[] CreateMultiple<T>(int cnt) where T : GUIControl
    {
        T[] ctls = new T[cnt];
        for (int i = 0; i < cnt; ++i)
        {
            ctls[i] = Create<T>();
        }
        return ctls;
    }

    public void Destroy(GUIControl ctl)
    {
        OnDestroyControl(ctl);
        GameObject.Destroy(ctl.gameObject);
    }
}
