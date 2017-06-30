using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrStuntman : MonoBehaviour {

    public float rebalanceRate = 1f;
    public float rebalanceTimeDetla;

	// Use this for initialization
	void Start () {
        rebalanceTimeDetla = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateBalance();
	}

    public void PositiveRotation() {
        this.transform.Rotate(Vector3.forward * 2);
        rebalanceTimeDetla = 0f;
    }

    public void NegativeRotation() {
        this.transform.Rotate(Vector3.back * 2);
        rebalanceTimeDetla = 0f;
    }

    void UpdateBalance() {
        int balanceAngle = Mathf.RoundToInt(this.transform.rotation.eulerAngles.z);

        if (balanceAngle != 0) {
            rebalanceTimeDetla += Time.deltaTime;

            if (rebalanceTimeDetla >= rebalanceRate) {
                if (balanceAngle > 270) {
                    PositiveRotation();
                }
                else {
                    NegativeRotation();
                }
            }
        }
    }
}
