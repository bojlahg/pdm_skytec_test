using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIMenu : GUITemplate
{
    public Text m_TitleText;
    public GameObject m_ButtonPrefab;

    public void SetTitle(string t)
    {
        m_TitleText.text = t;
    }
}
