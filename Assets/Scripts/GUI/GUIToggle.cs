using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIToggle : GUIControl
{
    public delegate void OnValueChangedEvent(GUIControl ctl, bool v);
    public OnValueChangedEvent onValueChanged;

    public Text m_CaptionText;

    private Toggle m_Toggle;

    public override void Init()
    {
        base.Init();
        m_Toggle = GetComponent<Toggle>();
        m_Toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool v)
    {
        if (onValueChanged != null)
        {
            onValueChanged(this, v);
        }
    }

    public void SetCaption(string t)
    {
        if (t == null || t.Length == 0)
        {
            m_CaptionText.gameObject.SetActive(false);
        }
        else
        {
            m_CaptionText.gameObject.SetActive(true);
            m_CaptionText.text = t;
        }
    }

    public void SetCheckState(bool c)
    {
        m_Toggle.isOn = c;
    }

    public bool GetCheckState()
    {
        return m_Toggle.isOn;
    }
}
