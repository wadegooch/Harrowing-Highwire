using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {
    public float flightSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(flightSpeed, -1.8f) * Time.deltaTime;
	}

    private void OnBecameInvisible() {
        Destroy(this);
    }
}
