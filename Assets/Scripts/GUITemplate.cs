using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITemplate : MonoBehaviour
{
    public delegate void OnShowStart();
    public delegate void OnShowFinish();
    public delegate void OnHideStart();
    public delegate void OnHideFinish();

    public OnShowStart onShowStart;
    public OnShowFinish onShowFinish;
    public OnHideStart onHideStart;
    public OnHideFinish onHideFinish;

    virtual public void OnShow()
    {
        gameObject.SetActive(true);
    }

    virtual public void OnHide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        OnShow();
    }

    public void Hide()
    {
        OnHide();
    }
}
