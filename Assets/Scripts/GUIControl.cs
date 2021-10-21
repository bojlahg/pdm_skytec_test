using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIControl : MonoBehaviour
{
    [HideInInspector]
    public Animator m_Animator;
    public delegate void OnAppearStart(GUIControl ctl);
    public delegate void OnAppearFinish(GUIControl ctl);
    public delegate void OnDisappearStart(GUIControl ctl);
    public delegate void OnDisappearFinish(GUIControl ctl);

    public OnAppearStart onAppearStart;
    public OnAppearFinish onAppearFinish;
    public OnDisappearStart onDisappearStart;
    public OnDisappearFinish onDisappearFinish;

    public virtual void OnInit()
    {

    }

    public void Init()
    {
        m_Animator = GetComponent<Animator>();
        OnInit();
    }


    public void Show()
    {
        if (m_Animator == null)
        {
            if (onAppearStart != null)
            {
                onAppearStart.Invoke(this);
            }
            gameObject.SetActive(true);
            if (onAppearFinish != null)
            {
                onAppearFinish.Invoke(this);
            }
        }
        else
        {
            if (onAppearStart != null)
            {
                onAppearStart.Invoke(this);
            }
            gameObject.SetActive(true);
            m_Animator.SetBool("Visible", true);
        }
    }

    public void Hide()
    {
        if (m_Animator == null)
        {
            if (onDisappearStart != null)
            {
                onDisappearStart.Invoke(this);
            }
            gameObject.SetActive(true);
            if (onDisappearFinish != null)
            {
                onDisappearFinish.Invoke(this);
            }
        }
        else
        {
            if (onDisappearStart != null)
            {
                onDisappearStart.Invoke(this);
            }
            m_Animator.SetBool("Visible", false);
        }
    }

    public void AnimationAppearFinish()
    {
        if (onAppearFinish != null)
        {
            onAppearFinish.Invoke(this);
        }
    }

    public void AnimationDisappearFinish()
    {
        gameObject.SetActive(false);
        if (onDisappearFinish != null)
        {
            onDisappearFinish.Invoke(this);
        }
    }
}
