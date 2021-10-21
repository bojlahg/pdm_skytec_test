using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIControl : MonoBehaviour
{
    public delegate void OnAppearStart(GUIControl ctl);
    public delegate void OnAppearFinish(GUIControl ctl);
    public delegate void OnDisappearStart(GUIControl ctl);
    public delegate void OnDisappearFinish(GUIControl ctl);

    public OnAppearStart onAppearStart;
    public OnAppearFinish onAppearFinish;
    public OnDisappearStart onDisappearStart;
    public OnDisappearFinish onDisappearFinish;

    private Animator m_Animator;

    public virtual void Init()
    {
        m_Animator = GetComponent<Animator>();
    }

    public virtual void Show()
    {
        if (m_Animator == null)
        {
            AppearStart();
            gameObject.SetActive(true);
            AppearFinish();
        }
        else
        {
            AppearStart();
            gameObject.SetActive(true);
            m_Animator.SetBool("Visible", true);
        }
    }

    public virtual void Hide()
    {
        if (m_Animator == null)
        {
            DisappearStart();
            gameObject.SetActive(true);
            DisappearFinish();
        }
        else
        {
            DisappearStart();
            m_Animator.SetBool("Visible", false);
        }
    }

    public virtual void AppearStart()
    {
        if (onAppearStart != null)
        {
            onAppearStart.Invoke(this);
        }
    }

    public virtual void DisappearStart()
    {
        if (onDisappearStart != null)
        {
            onDisappearStart.Invoke(this);
        }
    }

    public virtual void AppearFinish()
    {
        if (onAppearFinish != null)
        {
            onAppearFinish.Invoke(this);
        }
    }

    public virtual void DisappearFinish()
    {
        gameObject.SetActive(false);
        if (onDisappearFinish != null)
        {
            onDisappearFinish.Invoke(this);
        }
    }

    public void AnimationAppearFinish()
    {
        AppearFinish();
    }

    public void AnimationDisappearFinish()
    {
        DisappearFinish();
    }
}
