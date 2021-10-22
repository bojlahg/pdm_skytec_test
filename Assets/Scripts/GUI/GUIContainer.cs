using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIContainer : GUIControl
{
    public delegate void OnBackKeyDown();
    public OnBackKeyDown onBackKeyDown;

    public CanvasGroup m_CanvasGroup;
    public Transform m_ControlRoot;


    public override void Init()
    {
        base.Init();
    }

    public override void Show()
    {
        //GUIController.instance.Push(this);
        base.Show();
    }

    public override void Hide()
    {
        //GUIController.instance.Pop();
        base.Hide();
    }

    public override void AppearStart()
    {
        GUIManager.instance.Push(this);
        m_CanvasGroup.blocksRaycasts = false;
        base.AppearStart();
    }

    public override void AppearFinish()
    {
        m_CanvasGroup.blocksRaycasts = true;
        base.AppearFinish();
    }

    public override void DisappearStart()
    {
        m_CanvasGroup.blocksRaycasts = false;
        base.DisappearStart();
    }

    public override void DisappearFinish()
    {
        GUIManager.instance.Pop();
        m_CanvasGroup.blocksRaycasts = true;
        base.DisappearFinish();
    }

    public void GotFocus()
    {
        m_CanvasGroup.blocksRaycasts = true;
    }

    public void LostFocus()
    {
        m_CanvasGroup.blocksRaycasts = false;
    }

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
