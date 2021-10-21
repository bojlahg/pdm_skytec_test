using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIToggle : GUIControl
{
    public delegate void OnValueChanged(GUIControl ctl, bool v);
    public OnValueChanged onValueChanged;

    [HideInInspector]
    public Toggle m_Toggle;
    public Text m_CaptionText;

    public override void Init()
    {
        base.Init();
        m_Toggle = GetComponent<Toggle>();
        m_Toggle.onValueChanged.AddListener((v) => { if (onValueChanged != null) { onValueChanged(this, v); } });
    }

    public void SetCaption(string t)
    {
        m_CaptionText.text = t;
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
