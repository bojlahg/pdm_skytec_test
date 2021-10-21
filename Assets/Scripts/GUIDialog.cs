using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIDialog : MonoBehaviour
{
    public Text m_TitleText;
    public GameObject m_ButtonPrefab;

    public void SetTitle(string t)
    {
        m_TitleText.text = t;
    }

    public GUIButton CreateButton()
    {
        GameObject go = GameObject.Instantiate(m_ButtonPrefab, m_ButtonPrefab.transform.parent);
        return go.GetComponent<GUIButton>();
    }

    public void Destroy(GUITemplate tmp)
    {
        GameObject.Destroy(tmp.gameObject);
    }
}
