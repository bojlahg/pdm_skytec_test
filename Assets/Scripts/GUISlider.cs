using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUISlider : GUIControl
{
    public delegate void OnValueChanged(float v);
    public OnValueChanged onValueChanged;

    [HideInInspector]
    public Slider m_Slider;
    public Text m_CaptionText;

    public override void OnInit()
    {
        m_Slider = GetComponentInChildren<Slider>();
        m_Slider.onValueChanged.AddListener((v) => { if (onValueChanged != null) { onValueChanged(v); } });
    }

    public void SetCaption(string t)
    {
        m_CaptionText.text = t;
    }

    public void SetCheckState(float v)
    {
        m_Slider.value = v;
    }

    public float GetCheckState()
    {
        return m_Slider.value;
    }
}
