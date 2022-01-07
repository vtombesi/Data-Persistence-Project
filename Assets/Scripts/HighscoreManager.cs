using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class HighscoreManager : MonoBehaviour
{

    private GameObject highscoreContainer;

    [SerializeField] GameObject entryPrefab;
    [SerializeField] Button addButton;

    // Start is called before the first frame update
    void Start()
    {
        addButton.onClick.AddListener(() => addButtonClicked("John", 25));
        highscoreContainer = GameObject.Find("Highscore Container");
        PopulateHighscore();
    }

    private void addButtonClicked(string name, int score)
    {
        StartCoroutine(SubmitHighscore(name, score));
    }

    private IEnumerator SubmitHighscore(string name, int score)
    {
        ApplicationState.Instance.SubmitForHighscore(name, score);
        yield return new WaitForSeconds(2.0f);
        PopulateHighscore();
    }

    public void PopulateHighscore()
    {
        if (ApplicationState.Instance != null && ApplicationState.Instance.Highscore.Count > 0)
        {
            for (int i = 0; i < highscoreContainer.transform.childCount; i++)
            {
                GameObject child = highscoreContainer.transform.GetChild(i).gameObject;
                if (i < ApplicationState.Instance.Highscore.Count)
                {
                    ApplicationState.HighscoreEntry entry = ApplicationState.Instance.Highscore[i];
                    child.GetComponent<HighscoreEntry>().setEntry(entry.name, entry.score);
                    child.SetActive(true);
                } else
                {
                    child.SetActive(false);
                }
            }
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0); 
    }
}
