using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public MrStuntman mrStuntMan;
    public Text stepText;
    public Text scoreText;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    private float stepRate = 0.5f;
    private float fallTimer = 2f;
    private bool isPaused = false;
    private int steps;
    private float stepTimeDelta;

    // Use this for initialization
    void Start() {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        steps = 1;
        UpdateSteps();
        Input.multiTouchEnabled = true;
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
            // Allow Mr Stuntman to fall before displaying game over
            mrStuntMan.GetComponent<BoxCollider2D>().enabled = false;
            fallTimer -= Time.deltaTime;
            if (fallTimer <= 0.0f) {
                // Stop the animations
                Time.timeScale = 0f;
                gameOverPanel.SetActive(true);
                UpdateSteps();
                scoreText.text += "\n" + steps.ToString();
                fallTimer = 2f;
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
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame() {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    private void UpdateSteps() {
        stepText.text = "Steps  " + steps;
    }
}
