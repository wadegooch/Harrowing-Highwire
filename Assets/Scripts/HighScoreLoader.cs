using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HighScoreLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		LoadScores();
	}

	// Update is called once per frame
	void Update () {

	}

    private bool LoadScores() {
        try {
            string line;
            StreamReader sr = new StreamReader("Assets/Data/highscores.csv", Encoding.Default);

            using (sr) {
                int rank = 1;
                do {
                    line = sr.ReadLine();

                    if (line != null) {
                        string[] entries = line.Split(',');
                        if (entries.Length > 0)
                            GenerateScoreText(rank, entries[0], entries[1]);
                    }
                    rank++;
                }
                while (line != null);
                sr.Close();
                return true;
            }
        }
        catch (Exception e) {
            Debug.Log(e.Message);
            return false;
        }
    }

    private void GenerateScoreText(int rank, string name, string score) {
		GameObject ngo = new GameObject("myTextGO");
		ngo.transform.SetParent(this.transform);
		Text text = ngo.AddComponent<Text>();

		//Font setup
		Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
		text.font = ArialFont;
		text.fontSize = 24;
		text.material = ArialFont.material;
		text.color = Color.black;

		//Positioning
		this.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
		text.GetComponent<RectTransform>().position = new Vector3 (0.0f, Screen.height/2 - rank * 25.0f, 0.0f);
		text.GetComponent<RectTransform>().sizeDelta = new Vector2 (200.0f, 50.0f);

		//Text
		text.text = String.Format("{0:0#}. {1} : {2}", rank, name, score);
	}
}
