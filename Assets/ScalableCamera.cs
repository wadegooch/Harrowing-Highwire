using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableCamera : MonoBehaviour {

    public float targetWidth = 1080f;
    public float targetHeight = 1920f;
    public int pixelsToUnits = 30;

	// Use this for initialization
	void Start () {
        float desiredRatio = targetWidth / targetHeight;
        float currentRatio = (float) Screen.width / (float) Screen.height;

        if (currentRatio >= desiredRatio) {
            Camera.main.orthographicSize = (targetHeight / 4) / pixelsToUnits;
        }
        else {
            float differenceInSize = desiredRatio / currentRatio;
            Camera.main.orthographicSize = ((targetHeight / 4) / pixelsToUnits) * differenceInSize;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
