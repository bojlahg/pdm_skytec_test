using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIProgressBar : GUIControl
{
    public Image m_FillImage;
    public Text m_Text;

    public void SetProgress(float p)
    {
        m_FillImage.fillAmount = p;
    }

    public void SetText(string t)
    {
        if (t == null || t.Length == 0)
        {
            m_Text.gameObject.SetActive(false);
        }
        else
        {
            m_Text.gameObject.SetActive(true);
            m_Text.text = t;
        }
    }
}
