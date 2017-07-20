using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

public class HighScoreController : MonoBehaviour {

    public List<Text> highScoreTextFields;

    private const int HIGH_SCORE_CAPACITY = 10;
    private List<KeyValuePair<string, int>> highScoreList = new List<KeyValuePair<string, int>>();
    private string dataFilePath;

    // Use this for initialization
    void Start () {
        dataFilePath = Application.persistentDataPath + "/highscores.csv";
        LoadScores();
        if (highScoreTextFields.Any()) {
            PrintScores();
        }
	}

	// Update is called once per frame
	void Update () {

	}

    public bool IsNewHighScore(int score) {
        bool isHighScore = false;

        if (highScoreList.Count < HIGH_SCORE_CAPACITY) {
            isHighScore = true;
        }
        else {
            foreach (KeyValuePair<string, int> kvp in highScoreList) {
                if (score > kvp.Value) {
                    isHighScore = true;
                    break;
                }
            }
        }

        return isHighScore;
    }

    public void SaveNewScore(KeyValuePair<string, int> kvp) {
        for (int i = 0; i < HIGH_SCORE_CAPACITY; i++) {
            if (highScoreList.Count != 0 && i < highScoreList.Count) {
                if (kvp.Value > highScoreList[i].Value) {
                    highScoreList.Insert(i, kvp);
                    if (highScoreList.Count > HIGH_SCORE_CAPACITY) {
                        highScoreList.RemoveAt(highScoreList.Count - 1);
                    }
                    break;
                }
            }
            else {
                highScoreList.Insert(i, kvp);
                break;
            }
        }

        int index = 0;
        try {
            string line;
            FileStream fs = new FileStream(dataFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);

            using (sw) {
                do {
                    line = highScoreList[index].Key + "," + highScoreList[index].Value.ToString();
                    sw.WriteLine(line);
                    index++;
                }
                while (index < highScoreList.Count);
                sw.Close();
            }
        }
        catch (Exception e) {
            Debug.LogError(e.Message);
        }
    }

    private void LoadScores() {
        try {
            string line;
            FileStream fs = new FileStream(dataFilePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs, Encoding.Default);

            using (sr) {
                do {
                    line = sr.ReadLine();

                    if (line != null) {
                        string[] entries = line.Split(',');
                        if (entries.Length > 0) {
                            KeyValuePair<string, int> kvp = new KeyValuePair<string, int>(entries[0], Convert.ToInt32(entries[1]));
                            highScoreList.Add(kvp);
                        }
                    }
                }
                while (line != null);
                sr.Close();
            }
            // Sort the list descending
            highScoreList.Sort((n, m) => m.Value.CompareTo(n.Value));
        }
        catch (Exception e) {
            Debug.LogError(e.Message);
        }
    }

    private void PrintScores() {
        for (int i = 0; i < highScoreList.Count; i++) {
            highScoreTextFields[i].text += highScoreList[i].Value + ", " + highScoreList[i].Key;
        }
    }
}
