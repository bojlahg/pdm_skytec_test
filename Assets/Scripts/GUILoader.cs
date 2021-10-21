using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUILoader : GUITemplate
{
    public Text m_TitleText, m_ProgressBarText;
    public Image m_ProgressFill;

    public void SetTitle(string t)
    {
        m_TitleText.text = t;
    }

    public void SetProgressText(string t)
    {
        m_ProgressBarText.text = t;
    }

    public void SetProgress(float a)
    {
        m_ProgressFill.fillAmount = a;
    }
}
