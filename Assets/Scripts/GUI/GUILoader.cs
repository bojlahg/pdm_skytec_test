using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUILoader : GUIPanel
{
    public Text m_TitleText, m_ProgressBarText;
    public Image m_ProgressFill;

    public void SetTitle(string t)
    {
        m_TitleText.text = t;
    }
}
