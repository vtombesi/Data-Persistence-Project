using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreEntry : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text scoreText;

    public void setEntry(string name, int score)
    {
        nameText.text = name;
        scoreText.text = score.ToString();
    }
}
