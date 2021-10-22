using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyExitDialog : MonoBehaviour
{
    public Sprite m_YesIconSprite, m_NoIconSprite;

    private GUIDialog m_Dialog;

    public void Create()
    {
        m_Dialog = GUIManager.instance.Create<GUIDialog>(1);
        m_Dialog.SetTitle("Ой-Ой!");
        m_Dialog.SetMessage("Вы точно хотите выйти из игры?");
        m_Dialog.onBackKeyDown = NoButton_Click;
        m_Dialog.onDisappearFinish = GUIManager.instance.Destroy;

        GUIButton yesButton = m_Dialog.Create<GUIButton>();
        yesButton.SetCaption("Да");
        yesButton.SetIcon(m_YesIconSprite);
        yesButton.onButtonClick = YesButton_Click;
        yesButton.Show();

        GUIButton noButton = m_Dialog.Create<GUIButton>();
        noButton.SetCaption("Нет");
        noButton.SetIcon(m_NoIconSprite);
        noButton.onButtonClick = NoButton_Click;
        noButton.Show();

        m_Dialog.Show();
    }

    public void Free()
    {
        m_Dialog.Hide();
    }

    private void YesButton_Click()
    {
        Application.Quit();
    }

    private void NoButton_Click()
    {
        m_Dialog.Hide();
    }
}
