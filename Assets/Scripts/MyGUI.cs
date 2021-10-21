using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGUI : MonoBehaviour
{
    public string[] m_ProgressTexts;


    private GUIMenu m_MainMenu, m_SettingsMenu, m_CreditsMenu;
    private GUIDialog m_ExitDialog;

    private void Start()
    {
        StartCoroutine(LoadingProgressSimulation());
    }

    private IEnumerator LoadingProgressSimulation()
    {
        GUILoader loader = GUIController.instance.CreateLoader();
        loader.SetTitle("Загрузка");
        loader.SetProgressText("Загружаем ресурсы...");
        loader.SetProgress(0);
        loader.onDisappearFinish = InitialLoaderFinished;
        loader.Show();

        float timer = 0;
        int textIndex = 0, prevTextIndex = -1;
        while (timer < m_ProgressTexts.Length)
        {
            textIndex = Mathf.FloorToInt(timer);
            if (textIndex != prevTextIndex && textIndex < m_ProgressTexts.Length)
            {
                prevTextIndex = textIndex;
                loader.SetProgressText(m_ProgressTexts[textIndex]);
            }
            loader.SetProgress(timer / (float)m_ProgressTexts.Length);
            yield return null;
            timer += Time.deltaTime;
        }
        loader.Hide();
        GUIController.instance.Destroy(loader);
    }

    private void InitialLoaderFinished(GUIControl ctl)
    {
        MainMenu_Create();
    }

    private void MainMenu_Create()
    {
        m_MainMenu = GUIController.instance.CreateMenu();
        m_MainMenu.SetTitle("Главное меню");
        m_MainMenu.onDisappearFinish = DisappearDestroy;

        GUIButton buttonPlay = m_MainMenu.CreateButton();

        buttonPlay.SetCaption("Играть");
        buttonPlay.onButtonClick = MainMenu_PlayButton_Click;
        buttonPlay.Show();

        GUIButton buttonSettings = m_MainMenu.CreateButton();
        buttonSettings.SetCaption("Настройки");
        buttonSettings.onButtonClick = MainMenu_SettingsButton_Click;
        buttonSettings.Show();

        GUIButton buttonCredits = m_MainMenu.CreateButton();
        buttonCredits.SetCaption("О Игре");
        buttonCredits.onButtonClick = MainMenu_CreditsButton_Click;
        buttonCredits.Show();

        GUIButton backButton = m_MainMenu.CreateButton();
        backButton.SetCaption("Назад");
        backButton.onButtonClick = MainMenu_BackButton_Click;
        backButton.Show();

        m_MainMenu.Show();
    }

    private void DisappearDestroy(GUIControl ctl)
    {
        GUIController.instance.Destroy(ctl);
    }

    private void MainMenu_PlayButton_Click()
    {
        m_MainMenu.Hide();
    }

    private void MainMenu_SettingsButton_Click()
    {
        SettingsMenu_Create();
        m_MainMenu.Hide();
    }

    private void MainMenu_CreditsButton_Click()
    {
        CreditsMenu_Create();       
        m_MainMenu.Hide();
    }

    private void MainMenu_BackButton_Click()
    {
        ExitDialog_Create();
    }

    private void SettingsMenu_Create()
    {
        m_SettingsMenu = GUIController.instance.CreateMenu();
        m_SettingsMenu.SetTitle("Настройки");
        m_SettingsMenu.onDisappearFinish = DisappearDestroy;

        GUISlider volumeSlider = m_SettingsMenu.CreateSlider();
        volumeSlider.SetCaption("Громкость");
        volumeSlider.Show();

        GUIToggle musicToggle = m_SettingsMenu.CreateToggle();
        musicToggle.SetCaption("Музыка");
        musicToggle.Show();

        GUIButton backButton = m_SettingsMenu.CreateButton();
        backButton.SetCaption("Назад");
        backButton.onButtonClick = SettingsMenu_BackButton_Click;
        backButton.Show();

        m_SettingsMenu.Show();
    }

    private void SettingsMenu_BackButton_Click()
    {
        MainMenu_Create();
        m_SettingsMenu.Hide();
    }

    private void CreditsMenu_Create()
    {
        m_CreditsMenu = GUIController.instance.CreateMenu();
        m_CreditsMenu.SetTitle("О Игре");
        m_CreditsMenu.onDisappearFinish = DisappearDestroy;

        GUIText creditsText = m_CreditsMenu.CreateText();
        creditsText.SetText("Дмитрий Приходько\nbojlahg@gmail.com");
        creditsText.Show();

        GUIButton backButton = m_CreditsMenu.CreateButton();
        backButton.SetCaption("Назад");
        backButton.onButtonClick = CreditsMenu_BackButton_Click;
        backButton.Show();

        m_CreditsMenu.Show();
    }

    private void CreditsMenu_BackButton_Click()
    {
        MainMenu_Create();
        m_CreditsMenu.Hide();
    }

    private void ExitDialog_Create()
    {
        m_ExitDialog = GUIController.instance.CreateDialog();
        m_ExitDialog.SetTitle("Ой-Ой!");
        m_ExitDialog.SetMessage("Вы точно хотите выйти из игры?");

        GUIButton yesButton = m_ExitDialog.CreateButton();
        yesButton.SetCaption("Да");
        yesButton.onButtonClick = ExitDialog_YesButton_Click;
        yesButton.Show();

        GUIButton noButton = m_ExitDialog.CreateButton();
        noButton.SetCaption("Нет");
        noButton.onButtonClick = ExitDialog_NoButton_Click;
        noButton.Show();        

        m_ExitDialog.onDisappearFinish = DisappearDestroy;

        m_ExitDialog.Show();
    }

    private void ExitDialog_YesButton_Click()
    {
        Application.Quit();
    }

    private void ExitDialog_NoButton_Click()
    {
        m_ExitDialog.Hide();
    }
}
