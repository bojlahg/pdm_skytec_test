using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyYesNoDialog : MonoBehaviour, IGUILogic
{
    public delegate void OnAnswer(int aidx);
    public OnAnswer onAnswer;

    public Sprite m_YesIconSprite, m_NoIconSprite;

    private GUIDialog m_Dialog;

    public GUIPanel GetPanel()
    {
        if (m_Dialog == null)
        {
            m_Dialog = GUIManager.instance.Create<GUIDialog>(this, "Dialog", 1);

            m_Dialog.onBackKeyDown = YesButton_Click;
            m_Dialog.onDisappearFinish = GUIManager.instance.Destroy;

            GUIButton yesButton = m_Dialog.Create<GUIButton>("Button");
            yesButton.SetCaption("Да");
            yesButton.SetIcon(m_YesIconSprite);
            yesButton.onButtonClick = YesButton_Click;
            yesButton.Show();

            GUIButton noButton = m_Dialog.Create<GUIButton>("Button");
            noButton.SetCaption("Нет");
            noButton.SetIcon(m_NoIconSprite);
            noButton.onButtonClick = NoButton_Click;
            noButton.Show();
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

    private void YesButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        m_Dialog.Hide();
        if (onAnswer != null)
        {
            onAnswer.Invoke(0);
        }
        onAnswer = null;
    }

    private void NoButton_Click()
    {
        SoundManager.instance.PlayOnce("ButtonClick");

        m_Dialog.Hide();
        if (onAnswer != null)
        {
            onAnswer.Invoke(1);
        }
        onAnswer = null;
    }
}
