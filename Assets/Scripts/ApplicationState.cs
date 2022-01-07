using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ApplicationState : MonoBehaviour
{
    public static ApplicationState Instance;

    public List<HighscoreEntry> Highscore;

    public string NickName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadTable();
    }

    private void CreateDefaultHighscore()
    {
        Highscore = new List<HighscoreEntry>();

        Highscore.Add(new HighscoreEntry("Ted",     0));
        Highscore.Add(new HighscoreEntry("Ed",      0));
        Highscore.Add(new HighscoreEntry("John",    0));
        Highscore.Add(new HighscoreEntry("Charles", 0));
        Highscore.Add(new HighscoreEntry("Aileen",  0));
        Highscore.Add(new HighscoreEntry("Dexter",  0));
        Highscore.Add(new HighscoreEntry("Jeffrey", 0));
        Highscore.Add(new HighscoreEntry("Joseph",  0));
        Highscore.Add(new HighscoreEntry("Gary",    0));
        Highscore.Add(new HighscoreEntry("Donald",  0));

        SaveTable();
    }

    public void SubmitForHighscore(string name, int score)
    {
        HighscoreEntry newScore = new HighscoreEntry(name, score);

        Highscore.Insert(0, newScore);
        Highscore.Sort((p, q) => q.score.CompareTo(p.score));

        if (Highscore.Count > 10)
        {
            Highscore.RemoveRange(10, Highscore.Count - 10); 
        }

        SaveTable();
    }

    public void SaveTable()
    {
        SaveData data = new SaveData();
        data.Highscore = Highscore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highscores.json", json);
    }

    public void LoadTable()
    {
        string path = Application.persistentDataPath + "/highscores.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Highscore = data.Highscore; 
        } else
        {
            CreateDefaultHighscore();
        }
    }

    [System.Serializable]
    class SaveData
    {
        public List<HighscoreEntry> Highscore;
    }

    [System.Serializable]
    public struct HighscoreEntry
    {
        public string name;
        public int score;

        public HighscoreEntry(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }

}
