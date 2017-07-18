using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public MrStuntman mrStuntMan;
    public HighScoreController highScoreController;
    public Text stepText;
    public Text goScoreText;
    public Text nhsScoreText;
    public InputField highScoreInput;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject highScorePanel;

    private float stepRate = 0.5f;
    private float fallTimer = 2f;
    private bool isPaused = false;
    private string sfxPrefsKey = "AreSoundEffectsOn";
    private int steps;
    private float stepTimeDelta;

    // Use this for initialization
    void Start() {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        highScorePanel.SetActive(false);
        steps = 1;
        UpdateSteps();
        Input.multiTouchEnabled = true;
        if (PlayerPrefs.HasKey(sfxPrefsKey)) {
            if (PlayerPrefs.GetInt(sfxPrefsKey) > 0) {
                AudioListener.pause = true;
            }
            else {
                AudioListener.pause = false;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (!mrStuntMan.hasFallen) {
            // Simulate touch events from mouse events
            HandleTouchInput();

            stepTimeDelta += Time.deltaTime;

            if (stepTimeDelta >= stepRate) {
                stepTimeDelta = 0f;
                steps++;
                UpdateSteps();
            }
        }
        else {
            // Allow Mr Stuntman to fall before displaying game over or high score
            mrStuntMan.GetComponent<BoxCollider2D>().enabled = false;
            fallTimer -= Time.deltaTime;
            if (fallTimer <= 0.0f) {
                // Stop the animations
                Time.timeScale = 0f;
                UpdateSteps();
                fallTimer = 2f;
                if (highScoreController.IsNewHighScore(steps)) {
                    nhsScoreText.text += "\n" + steps.ToString();
                    highScorePanel.SetActive(true);
                }
                else {
                    goScoreText.text += "\n" + steps.ToString();
                    gameOverPanel.SetActive(true);
                }
            }
        }
    }

    void HandleTouchInput() {
        if (!isPaused) {
            // Mouse & keyboard controls
            if (Input.touchCount == 0) {
                if (Input.GetMouseButtonDown(0)) {
                    if (Input.mousePosition.x < Screen.width / 2) {
                        // Left Click
                        mrStuntMan.PositiveRotation();
                    }
                    else if (Input.mousePosition.x > Screen.width / 2) {
                        // Right Click
                        mrStuntMan.NegativeRotation();
                    }
                }

                if (Input.GetKeyDown("space")) {
                    mrStuntMan.Jump();
                }
                else if (Input.GetKeyDown("d")) {
                    mrStuntMan.Duck();
                }
            }
            else {
                // Touch controls left/right
                foreach (Touch touch in Input.touches) {
                    if (touch.position.x < Screen.width / 2) {
                        // Left Touch
                        mrStuntMan.PositiveRotation();
                    }
                    else if (touch.position.x > Screen.width / 2) {
                        // Right Touch
                        mrStuntMan.NegativeRotation();
                    }
                }

                // Touch controls swipe up/down
                for (int i = 0; i < Input.touchCount; i++) {

                    float startTouchPosition = 0f;
                    float endTouchPosition = 0f;
                    Touch touch = Input.GetTouch(i);

                    if (touch.phase == TouchPhase.Began) {
                        startTouchPosition = touch.position.y;
                    }
                    else if (touch.phase == TouchPhase.Ended) {
                        endTouchPosition = touch.position.y;
                        if (endTouchPosition > startTouchPosition) {
                            // Swipe Up
                            mrStuntMan.Jump();
                        }
                        else if (endTouchPosition < startTouchPosition) {
                            // Swipe Down
                            mrStuntMan.Duck();
                        }
                    }

                }
            }
        }
    }

    public void PauseGame() {
        isPaused = true;
        mrStuntMan.PositiveRotation();
        mrStuntMan.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame() {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void SubmitHighScore() {
        KeyValuePair<string, int> highScoreKVP = new KeyValuePair<string, int>(highScoreInput.text, steps);
        highScoreController.SaveNewScore(highScoreKVP);
        SceneManager.LoadScene("Main");
    }

    private void UpdateSteps() {
        stepText.text = "Steps  " + steps;
    }
}
