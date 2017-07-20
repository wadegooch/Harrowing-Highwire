using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public MrStuntman mrStuntman;
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
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private float dragThreshold;

    // Use this for initialization
    void Start() {
        Time.timeScale = 1f;
        dragThreshold = Screen.height * (15 / 100);
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
        if (!mrStuntman.hasFallen) {
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
            mrStuntman.GetComponent<BoxCollider2D>().enabled = false;
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
                        mrStuntman.PositiveRotation();
                    }
                    else if (Input.mousePosition.x > Screen.width / 2) {
                        // Right Click
                        mrStuntman.NegativeRotation();
                    }
                }

                if (Input.GetKeyDown("space")) {
                    mrStuntman.Jump();
                }
                else if (Input.GetKeyDown("d")) {
                    mrStuntman.Duck();
                }
            }
            else {
                // Get the single touch
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {
                    touchStartPos = touch.position;
                    touchEndPos = touch.position;
                }
                // Update the end position based on where the touch is after Update
                else if (touch.phase == TouchPhase.Moved) {
                    touchEndPos = touch.position;
                }
                // End of touch
                else if (touch.phase == TouchPhase.Ended) {
                    touchEndPos = touch.position;

                    // Check if drag distance is greater than 20% of the screen height
                    if (Mathf.Abs(touchEndPos.x - touchStartPos.x) > dragThreshold || Mathf.Abs(touchEndPos.y - touchStartPos.y) > dragThreshold) {
                        if (touchEndPos.y > touchStartPos.y) {
                            mrStuntman.Jump();
                        }
                        else {
                            mrStuntman.Duck();
                        }
                    }
                    // Input is a tap
                    else {
                        if (touchEndPos.x < Screen.width / 2) {
                            mrStuntman.PositiveRotation();
                        }
                        else {
                            mrStuntman.NegativeRotation();
                        }
                    }
                }
            }
        }
    }

    public void PauseGame() {
        isPaused = true;
        mrStuntman.PositiveRotation();
        mrStuntman.GetComponent<Rigidbody2D>().angularVelocity = 0f;
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
