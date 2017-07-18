using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

public class HighScoreController : MonoBehaviour {

    public List<Text> highScoreTextFields;

    private string dataFilePath = "Assets/Data/highscores.csv";
    private List<KeyValuePair<string, int>> highScoreList = new List<KeyValuePair<string, int>>();

	// Use this for initialization
	void Start () {
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
        foreach (KeyValuePair<string, int> kvp in highScoreList) {
            if (score > kvp.Value) {
                isHighScore = true;
                break;
            }
        }
        return isHighScore;
    }

    public void SaveNewScore(KeyValuePair<string, int> kvp) {
        for (int i = highScoreList.Count-1; i > 0; i--) {
            if (kvp.Value > highScoreList[i].Value) {
                highScoreList[i] = kvp;
                break;
            }
        }

        int index = 0;
        try {
            string line;
            StreamWriter sw = new StreamWriter(dataFilePath, false, Encoding.Default);

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
            StreamReader sr = new StreamReader(dataFilePath, Encoding.Default);

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
        for (int i = 0; i < highScoreTextFields.Count; i++) {
            highScoreTextFields[i].text += highScoreList[i].Value + ", " + highScoreList[i].Key;
        }
    }
}
