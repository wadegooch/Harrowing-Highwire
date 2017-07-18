using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private string tutorialPrefsKey = "IsTutorialOn";


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void LoadGame() {
        SceneManager.LoadScene("Main");
    }

    public void LoadMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void LoadHighScores() {
        SceneManager.LoadScene("HighScore");
    }

    public void LoadTutorial() {
        SceneManager.LoadScene("Tutorial");
    }

    public void TransitionFromMenu() {
        if (PlayerPrefs.HasKey(tutorialPrefsKey)) {
            if (PlayerPrefs.GetInt(tutorialPrefsKey) == 0) {
                LoadTutorial();
            }
            else {
                LoadGame();
            }
        }
        else {
            LoadTutorial();
        }
    }
}
