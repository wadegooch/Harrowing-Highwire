using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text stepText;
    public int steps;
    public float stepRate = 2f;
    public MrStuntman mrStuntMan;
    private float stepTimeDelta;

	// Use this for initialization
	void Start () {
        steps = 0;
        UpdateSteps();
        Input.multiTouchEnabled = true;
    }

    // Update is called once per frame
    void Update () {

        // Simulate touch events from mouse events
        HandleTouchInput();

        stepTimeDelta += Time.deltaTime;

        if (stepTimeDelta >= stepRate) {
            stepTimeDelta = 0f;
            UpdateSteps();
        }
	}

    void HandleTouchInput()
    {
        //Mouse & keyboard controlls
        if (Input.touchCount == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition.x < Screen.width / 2)
                {
                    //Left Click
                    mrStuntMan.PositiveRotation();
                }
                else if (Input.mousePosition.x > Screen.width / 2)
                {
                    //Right Click
                    mrStuntMan.NegativeRotation();
                }
            }

            if (Input.GetKeyDown("space")) {
                mrStuntMan.Jump();
            } else if (Input.GetKeyDown("d"))
            {
                mrStuntMan.Duck();
            }
        } else {

            // Touch controls left/right
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    // Left Touch
                    mrStuntMan.PositiveRotation();
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    // Right Touch
                    mrStuntMan.NegativeRotation();
                }
            }

            // Touch controls swipe up/down
            for (int i = 0; i < Input.touchCount; i++)
            {
                float startTouchPosition = 0f;
                float endTouchPosition = 0f;
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPosition = touch.position.y;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    endTouchPosition = touch.position.y;
                    if (endTouchPosition > startTouchPosition)
                    {
                        // Swipe Up
                        mrStuntMan.Jump();
                    }
                    else if (endTouchPosition < startTouchPosition)
                    {
                        // Swipe Down
                        mrStuntMan.Duck();
                    }
                }

            }
        }
    }

    void UpdateSteps() {
        stepText.text = "Steps  " + steps++;
    }
}
