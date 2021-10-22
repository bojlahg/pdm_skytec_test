using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIInput : GUIControl
{
    public delegate void OnValueChangedEvent(GUIControl ctl, string v);
    public delegate void OnEndEditEvent(GUIControl ctl, string v);

    public OnValueChangedEvent onValueChanged;
    public OnEndEditEvent onEndEdit;

    public Text m_CaptionText;

    private InputField m_InputField;

    public override void Init()
    {
        base.Init();
        m_InputField = GetComponent<InputField>();
        m_InputField.onValueChanged.AddListener(OnValueChanged);
        m_InputField.onEndEdit.AddListener(OnEndEdit);
    }

    private void OnValueChanged(string v)
    {
        if (onValueChanged != null)
        {
            onValueChanged(this, v);
        }
    }

    private void OnEndEdit(string v)
    {
        if (onEndEdit != null)
        {
            onEndEdit(this, v);
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

    public void SetText(string t)
    {
        m_InputField.text = t;
    }

    public string GetText()
    {
        return m_InputField.text;
    }

    public void SetHintText(string t)
    {
        if (t == null || t.Length == 0)
        {
            m_InputField.placeholder.gameObject.SetActive(false);
        }
        else
        {
            m_InputField.placeholder.gameObject.SetActive(true);
            ((Text)m_InputField.placeholder).text = t;
        }
    }
}
