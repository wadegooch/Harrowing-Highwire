using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

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
}
