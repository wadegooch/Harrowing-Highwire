using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public Toggle tutorialToggle;

    private string tutorialPrefsKey = "IsTutorialOn";


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void LoadMenu() {
        SceneManager.LoadScene("Main");
    }

    public void LoadGame() {
        SceneManager.LoadScene("Game");
    }

    public void LoadHighScores() {
        SceneManager.LoadScene("HighScore");
    }

    public void LoadTutorial() {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadSettings() {
        SceneManager.LoadScene("Settings");
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

    public void TransitionFromTutorial() {
        if (tutorialToggle.isOn) {
            PlayerPrefs.SetInt(tutorialPrefsKey, 1);
        }
        else {
            PlayerPrefs.SetInt(tutorialPrefsKey, 0);
        }
        PlayerPrefs.Save();
        LoadGame();
    }
}
