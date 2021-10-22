using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUISlider : GUIControl
{
    public delegate void OnValueChanged(GUIControl ctl, float v);
    public OnValueChanged onValueChanged;

    
    private Slider m_Slider;
    public Text m_CaptionText;

    public override void Init()
    {
        base.Init();
        m_Slider = GetComponentInChildren<Slider>();
        m_Slider.onValueChanged.AddListener((v) => { if (onValueChanged != null) { onValueChanged(this, v); } });
    }

    public void SetCaption(string t)
    {
        m_CaptionText.text = t;
    }

    public void SetValue(float v)
    {
        m_Slider.value = v;
    }

    public float GetValue()
    {
        return m_Slider.value;
    }
}
