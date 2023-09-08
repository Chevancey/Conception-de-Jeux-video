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
        if (File.Exists(path)) {
            using StreamReader stream = new(path);
            string content = stream.ReadToEnd();

            HighScores highScores = JsonUtility.FromJson<HighScores>(content);
            return highScores.highScoreItems;
        }
        return Array.Empty<HighScoreItem>();
    }

    public void WriteHighScore(int score, string name, DateTime date) {
        HighScores highScores;
        List<HighScoreItem> highscoreList;
        if (File.Exists(path)) {
            using StreamReader readStream = new(path);
            string content = readStream.ReadToEnd();
            Debug.Log(content);
            highScores = JsonUtility.FromJson<HighScores>(content);
        } else {
            highScores = new()
            {
                highScoreItems = Array.Empty<HighScoreItem>()
            };
        }
        //Debug.Log("score : "+score + " name: " + name + " date: " + date.ToString("dd/MM"));
        highscoreList = new(highScores.highScoreItems)
        {
            new HighScoreItem(score, name, date.ToString("dd/MM"))
        };
        highscoreList.Sort();
        highScores.highScoreItems = highscoreList.ToArray();

        using StreamWriter writeStream = new(path);
        string serializedList = JsonUtility.ToJson(highScores,true);

        writeStream.Write(serializedList);
    }

    public void UpdateHighScores() {
        if (File.Exists(path)) {
            HighScores highScores;
            List<HighScoreItem> highscoreList;
            
            using StreamReader readStream = new(path);
            string content = readStream.ReadToEnd();
            Debug.Log(content);
            highScores = JsonUtility.FromJson<HighScores>(content);
            readStream.Dispose();

            highscoreList = new(highScores.highScoreItems);
            highscoreList.Sort();
            highScores.highScoreItems = highscoreList.ToArray();

            using StreamWriter writeStream = new(path);
            string serializedList = JsonUtility.ToJson(highScores,true);

            writeStream.Write(serializedList);
        }
    }
}
