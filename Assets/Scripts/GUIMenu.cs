using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIMenu : GUIControl
{
    public Text m_TitleText;
    public GameObject m_ButtonPrefab, m_TextPrefab, m_TogglePrefab, m_SliderPrefab;

    public void SetTitle(string t)
    {
        m_TitleText.text = t;
    }

    public GUIButton CreateButton()
    {
        GameObject go = GameObject.Instantiate(m_ButtonPrefab, m_ButtonPrefab.transform.parent);
        GUIButton btn = go.GetComponent<GUIButton>();
        btn.Init();
        return btn;
    }

    public GUIText CreateText()
    {
        GameObject go = GameObject.Instantiate(m_TextPrefab, m_TextPrefab.transform.parent);
        GUIText txt = go.GetComponent<GUIText>();
        txt.Init();
        return txt;
    }

    public GUIToggle CreateToggle()
    {
        GameObject go = GameObject.Instantiate(m_TogglePrefab, m_TogglePrefab.transform.parent);
        GUIToggle tgl = go.GetComponent<GUIToggle>();
        tgl.Init();
        return tgl;
    }

    public GUISlider CreateSlider()
    {
        GameObject go = GameObject.Instantiate(m_SliderPrefab, m_SliderPrefab.transform.parent);
        GUISlider sld = go.GetComponent<GUISlider>();
        sld.Init();
        return sld;
    }

    public void Destroy(GUIControl ctl)
    {
        GameObject.Destroy(ctl.gameObject);
    }
}
