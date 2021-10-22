using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIButton : GUIControl
{
    public delegate void OnButtonClickEvent();
    public OnButtonClickEvent onButtonClick;

    public Image m_IconImage;
    public Text m_CaptionText;

    private Button m_Button;

    public override void Init()
    {
        base.Init();
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (onButtonClick != null)
        {
            onButtonClick();
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

    public void SetIcon(Sprite s)
    {
        if (s == null)
        {
            m_IconImage.gameObject.SetActive(false);
        }
        else
        {
            m_IconImage.gameObject.SetActive(true);
            m_IconImage.sprite = s;
        }
    }


}
