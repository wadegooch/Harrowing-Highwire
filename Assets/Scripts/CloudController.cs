using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {

    public float constantYSpeed = -0.4f;
	
    // Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.position +=  new Vector3(0f, constantYSpeed) * Time.deltaTime;
	}
}
