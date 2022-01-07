#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{

    [SerializeField] private Button playButton;
    [SerializeField] private InputField nicknameInput;
    [SerializeField] private Text recordText;

    private void Start()
    {
        playButton.interactable = false;
        nicknameInput.onValueChanged.AddListener(InputChanged);
        nicknameInput.onEndEdit.AddListener(InputChanged);

        LoadHighscores();
    }

    private void InputChanged(string nickname)
    {
        if (nickname.Length > 0)
        {
            playButton.interactable = true;
        } else
        {
            playButton.interactable = false;
        }

    }

    public void StartNew()
    {
        ApplicationState.Instance.NickName = nicknameInput.text;
        SceneManager.LoadScene(1);
    }

    public void GotoHighscores()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        ApplicationState.Instance.SaveTable();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    public void SaveHighscores()
    {
        ApplicationState.Instance.SaveTable();
    }

    public void LoadHighscores()
    {
        ApplicationState.Instance.LoadTable();
        // ColorPicker.SelectColor(ApplicationState.Instance.TeamColor);

        ApplicationState.HighscoreEntry first = ApplicationState.Instance.Highscore[0];
        recordText.text = $"{first.name} - {first.score}";
    }

}
