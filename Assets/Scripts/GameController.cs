using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text stepText;
    public int steps;
    public float stepRate = 2f;

    private float stepTimeDelta;

	// Use this for initialization
	void Start () {
        steps = 0;
        UpdateSteps();
	}
	
	// Update is called once per frame
	void Update () {
        stepTimeDelta += Time.deltaTime;

        if (stepTimeDelta >= stepRate) {
            stepTimeDelta = 0f;
            UpdateSteps();
        }
	}

    void UpdateSteps() {
        stepText.text = "Steps  " + steps++;
    }
}
