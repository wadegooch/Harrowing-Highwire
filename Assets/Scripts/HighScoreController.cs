using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

public class HighScoreController : MonoBehaviour {

    public List<Text> highScoreTextFields;

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

    private void LoadScores() {
        try {
            string line;
            StreamReader sr = new StreamReader("Assets/Data/highscores.csv", Encoding.Default);

            using (sr) {
                int rank = 1;
                do {
                    line = sr.ReadLine();

                    if (line != null) {
                        string[] entries = line.Split(',');
                        if (entries.Length > 0) {
                            KeyValuePair<string, int> kvp = new KeyValuePair<string, int>(entries[0], Convert.ToInt32(entries[1]));
                            highScoreList.Add(kvp);
                        }
                    }
                    rank++;
                }
                while (line != null);
                sr.Close();
            }
        }
        catch (Exception e) {
            Debug.Log(e.Message);
        }
    }

    private void PrintScores() {
        highScoreList.Sort((n, m) => n.Value.CompareTo(m.Value));
        for (int i = 0; i < highScoreTextFields.Count; i++) {
            highScoreTextFields[i].text += highScoreList[i].Value + ", " + highScoreList[i].Key;
        }
    }
}
