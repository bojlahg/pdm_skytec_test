using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIButton : GUIControl
{
    public delegate void OnButtonClick();
    public OnButtonClick onButtonClick;

    [HideInInspector]
    public Button m_Button;
    public Text m_CaptionText;

    public override void OnInit()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(() => { if (onButtonClick != null) { onButtonClick(); } });
    }

    public void SetCaption(string t)
    {
        m_CaptionText.text = t;
    }
}
