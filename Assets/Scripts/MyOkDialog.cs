using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOkDialog : MonoBehaviour, IGUILogic
{
    public delegate void OnAnswer(int aidx);
    public OnAnswer onAnswer;

    public Sprite m_OkIconSprite;

    private GUIDialog m_Dialog;

    public GUIPanel GetPanel()
    {
        if (m_Dialog == null)
        {
            m_Dialog = GUIManager.instance.Create<GUIDialog>(this, "Dialog", 1);

            m_Dialog.onBackKeyDown = OkButton_Click;
            m_Dialog.onDisappearFinish = GUIManager.instance.Destroy;

            GUIButton okButton = m_Dialog.Create<GUIButton>("Button");
            okButton.SetCaption("Ok");
            okButton.SetIcon(m_OkIconSprite);
            okButton.onButtonClick = OkButton_Click;
            okButton.Show();
        }
        return m_Dialog;
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
        SoundManager.instance.PlayOnce("ButtonClick");

        m_Dialog.Hide();
        if (onAnswer != null)
        {
            onAnswer.Invoke(0);
        }
    }
}
