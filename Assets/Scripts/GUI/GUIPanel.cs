using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPanel : GUIContainer
{
    public delegate void OnBackKeyDown();
    public OnBackKeyDown onBackKeyDown;

    public CanvasGroup m_CanvasGroup;

    public override void AppearStart()
    {
        GUIManager.instance.Push(this);
        m_CanvasGroup.blocksRaycasts = false;
        base.AppearStart();
    }

    public override void AppearFinish()
    {
        m_CanvasGroup.blocksRaycasts = true;
        base.AppearFinish();
    }

    public override void DisappearStart()
    {
        GUIManager.instance.Pop();
        m_CanvasGroup.blocksRaycasts = false;
        base.DisappearStart();
    }

    public override void DisappearFinish()
    {
        m_CanvasGroup.blocksRaycasts = true;
        base.DisappearFinish();
    }

    public void GotFocus()
    {
        m_CanvasGroup.blocksRaycasts = true;
    }

    public void LostFocus()
    {
        m_CanvasGroup.blocksRaycasts = false;
    }
}
