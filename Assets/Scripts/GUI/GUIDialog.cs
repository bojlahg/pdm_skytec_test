using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIDialog : GUIContainer
{
    public Text m_TitleText, m_MessageText;

    public void SetTitle(string t)
    {
        m_TitleText.text = t;
    }

    public void SetMessage(string t)
    {
        m_MessageText.text = t;
    }
}
