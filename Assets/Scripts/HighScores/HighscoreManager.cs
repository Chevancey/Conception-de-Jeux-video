using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager Instance { get; private set; }
    private string path;
    private string persistentPath;

    [Serializable]
    public class HighScoreItem : IComparable {
        public int score;
        public string name;
        public string date;

        public HighScoreItem(int score, string name, string date) {
            this.score = score;
            this.name = name;
            this.date = date;
        }

        public int CompareTo(object obj)
        {
            HighScoreItem other = obj as HighScoreItem;

            if (score > other.score) {
                return -1;
            }

            if (score < other.score) {
                return 1;
            }
            return 0;
        }
    }
    [Serializable]
    private class HighScores {
        public HighScoreItem[] highScoreItems;
    }

    [Serializable]
    private class HighScoreJsons {
        public string[] highScoreItems;
    }


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
        SetPath();
    }

    private void SetPath() {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "highscores.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "highscores.json";
    }

    public HighScoreItem[] ReadHighScores() {
        if (File.Exists(persistentPath)) {

            HighScores highScores = ReadJsonHighScore();
            return highScores.highScoreItems;
        }
        return Array.Empty<HighScoreItem>();
    }

    public void WriteHighScore(int score, string name, DateTime date) {
        HighScores highScores;
        if (File.Exists(persistentPath)) {
            highScores = ReadJsonHighScore();
        } else {
            highScores = new()
            {
                highScoreItems = Array.Empty<HighScoreItem>()
            };
        }
        //Debug.Log("score : "+score + " name: " + name + " date: " + date.ToString("dd/MM"));
        highScores.highScoreItems.Append(new HighScoreItem(score, name, date.ToString("dd/MM")));
        Array.Sort(highScores.highScoreItems);
        Debug.Log(highScores.highScoreItems[0]);
        WriteJsonHighScore(highScores);
    }

    private void WriteJsonHighScore(HighScores highscores) {
        HighScoreJsons toWrite = new() {
            highScoreItems = Array.Empty<string>()
        };
        foreach(HighScoreItem highScore in highscores.highScoreItems) {
            toWrite.highScoreItems.Append(JsonUtility.ToJson(highScore,true));
        }
        using StreamWriter writeStream = new(path);
        string serializedList = JsonUtility.ToJson(toWrite,true);
        //Debug.Log(toWrite.highScoreItems[0]);

        writeStream.Write(serializedList);
    }

    private HighScores ReadJsonHighScore() {
        HighScoreJsons highScoreJsons;
        HighScores highScores = new() {
            highScoreItems = Array.Empty<HighScoreItem>()
        };

        using StreamReader readStream = new(path);
        string content = readStream.ReadToEnd();

        highScoreJsons = JsonUtility.FromJson<HighScoreJsons>(content);
        foreach(string jsonHighScore in highScoreJsons.highScoreItems) {
            highScores.highScoreItems.Append(JsonUtility.FromJson<HighScoreItem>(jsonHighScore));
        }
        return highScores;
    }
}
