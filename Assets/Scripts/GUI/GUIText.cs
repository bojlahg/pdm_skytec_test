using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIText : GUIControl
{
    private Text m_Text;

    public override void Init()
    {
        base.Init();
        m_Text = GetComponent<Text>();
    }

    public void SetText(string t)
    {
        m_Text.text = t;
    }

    public void SetColor(Color c)
    {
        m_Text.color = c;
    }
}
