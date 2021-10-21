using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIDialog : GUIControl
{
    public Text m_TitleText, m_MessageText;
    public GameObject m_ButtonPrefab;

    public void SetTitle(string t)
    {
        m_TitleText.text = t;
    }

    public void SetMessage(string t)
    {
        m_MessageText.text = t;
    }

    public GUIButton CreateButton()
    {
        GameObject go = GameObject.Instantiate(m_ButtonPrefab, m_ButtonPrefab.transform.parent);
        GUIButton btn = go.GetComponent<GUIButton>();
        btn.Init();
        return btn;
    }

    public void Destroy(GUIControl ctl)
    {
        GameObject.Destroy(ctl.gameObject);
    }
}
