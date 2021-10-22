using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIRadioGroup : GUIContainer
{
    public delegate void OnValueChangedEvent(GUIControl ctl, int v);
    public OnValueChangedEvent onValueChanged;

    public Text m_CaptionText;

    private ToggleGroup m_ToggleGroup;
    private List<GUIRadio> m_RadioList = new List<GUIRadio>();
    private int m_PrevIndex = 0;

    public ToggleGroup toggleGroup { get { return m_ToggleGroup; } }

    public override void Init()
    {
        base.Init();
        m_ToggleGroup = GetComponent<ToggleGroup>();
    }

    private void OnValueChanged(int v)
    {
        Debug.LogFormat("OnValueChanged {0}", v);
        if (onValueChanged != null)
        {
            onValueChanged(this, v);
        }
    }

    private void ChildToggle_OnValueChanged(GUIControl ctl, bool v)
    {
        int newidx = GetRadioIndex();
        if (m_PrevIndex != newidx)
        {
            OnValueChanged(newidx);
            m_PrevIndex = newidx;
        }
    }

    public override void OnCreateControl(GUIControl ctl)
    {
        if(ctl is GUIRadio)
        {
            GUIRadio rdo = (GUIRadio)ctl;
            rdo.onValueChanged += ChildToggle_OnValueChanged;
            m_RadioList.Add(rdo);
        }
    }

    public override void OnDestroyControl(GUIControl ctl)
    {
        if (ctl is GUIRadio)
        {
            m_RadioList.Remove((GUIRadio)ctl);
        }
    }

    public void SetCaption(string t)
    {
        if (t == null || t.Length == 0)
        {
            m_CaptionText.gameObject.SetActive(false);
        }
        else
        {
            m_CaptionText.gameObject.SetActive(true);
            m_CaptionText.text = t;
        }
    }

    public void SetRadioIndex(int idx)
    {
        for (int i = 0; i < m_RadioList.Count; ++i)
        {
            m_RadioList[i].SetCheckState(i == idx);
        }
        m_PrevIndex = idx;
    }

    public int GetRadioIndex()
    {
        for (int i = 0; i < m_RadioList.Count; ++i)
        {
            if(m_RadioList[i].GetCheckState())
            {
                return i;
            }
        }
        return -1;
    }
}
