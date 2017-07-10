using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableCamera : MonoBehaviour {

    public float targetWidth = 1440f;
    public float targetHeight = 2560f;
    public int pixelsToUnits = 534;

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
