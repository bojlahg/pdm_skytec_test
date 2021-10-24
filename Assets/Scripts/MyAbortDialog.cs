using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAbortDialog : MonoBehaviour
{
    public Sprite m_YesIconSprite, m_NoIconSprite;

    private GUIDialog m_Dialog;

    public void Create()
    {
        m_Dialog = GUIManager.instance.Create<GUIDialog>(1);
        m_Dialog.SetTitle("Прервать?");
        m_Dialog.SetMessage("Вы тоно хотите покинуть начатую игру?");
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

    private void YesButton_Click()
    {
        m_Dialog.Hide();
        //MyApp.instance.m_MyPauseMenu.Free();
        MyApp.instance.m_MyGame.AbortGame();
    }

    private void NoButton_Click()
    {
        m_Dialog.Hide();
    }
}
