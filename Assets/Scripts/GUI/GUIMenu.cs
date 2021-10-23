using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIMenu : GUIPanel
{
    public Text m_TitleText;

    public void SetTitle(string t)
    {
        m_TitleText.text = t;
    }
}
