using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOkDialog : MonoBehaviour, IUserInterface
{
    public delegate void OnAnswer(int aidx);
    public OnAnswer onAnswer;

    public Sprite m_OkIconSprite;

    private GUIDialog m_Dialog;

    private void Create()
    {
        m_Dialog = GUIManager.instance.Create<GUIDialog>("Dialog", 1);
       
        m_Dialog.onBackKeyDown = OkButton_Click;
        m_Dialog.onDisappearFinish = GUIManager.instance.Destroy;

        GUIButton okButton = m_Dialog.Create<GUIButton>("Button");
        okButton.SetCaption("Ok");
        okButton.SetIcon(m_OkIconSprite);
        okButton.onButtonClick = OkButton_Click;
        okButton.Show();

        m_Dialog.Show();
    }

    private void Free()
    {
        GUIManager.instance.Destroy(m_Dialog);
    }

    public void Show()
    {
        Create();
        m_Dialog.Show();
    }

    public void Hide()
    {
        m_Dialog.Hide();
    }

    public void SetTitle(string t)
    {
        m_Dialog.SetTitle(t);
    }

    public void SetMessage(string m)
    {
        m_Dialog.SetMessage(m);
    }

    private void OkButton_Click()
    {
        m_Dialog.Hide();
        if (onAnswer != null)
        {
            onAnswer.Invoke(0);
        }
    }
}
