using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIContainer : GUIControl
{
    public delegate void OnBackKeyDown();
    public OnBackKeyDown onBackKeyDown;

    public CanvasGroup m_CanvasGroup;
    public Transform m_ControlRoot;
    public GameObject m_ButtonPrefab, m_TextPrefab, m_TogglePrefab, m_SliderPrefab;    

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

    public GUIButton CreateButton()
    {
        GameObject go = GameObject.Instantiate(m_ButtonPrefab, m_ControlRoot);
        GUIButton btn = go.GetComponent<GUIButton>();
        btn.Init();
        return btn;
    }

    public GUIText CreateText()
    {
        GameObject go = GameObject.Instantiate(m_TextPrefab, m_ControlRoot);
        GUIText txt = go.GetComponent<GUIText>();
        txt.Init();
        return txt;
    }

    public GUIToggle CreateToggle()
    {
        GameObject go = GameObject.Instantiate(m_TogglePrefab, m_ControlRoot);
        GUIToggle tgl = go.GetComponent<GUIToggle>();
        tgl.Init();
        return tgl;
    }

    public GUISlider CreateSlider()
    {
        GameObject go = GameObject.Instantiate(m_SliderPrefab, m_ControlRoot);
        GUISlider sld = go.GetComponent<GUISlider>();
        sld.Init();
        return sld;
    }

    public void Destroy(GUIControl ctl)
    {
        GameObject.Destroy(ctl.gameObject);
    }
}
