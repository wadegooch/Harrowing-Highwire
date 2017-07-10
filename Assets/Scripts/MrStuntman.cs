using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrStuntman : MonoBehaviour {

    public float rebalanceRate = 1f;
    public float rebalanceTimeDetla;
    public GameObject player;
    public float jumpDistance = 0.3f;
    public float duckScale = 0.4f;
    public float duckTimer = 0.0f;
    public bool ducking = false;
    public bool jumping = false;

    // Use this for initialization
    void Start() {
        player = (GameObject)this.gameObject;
        rebalanceTimeDetla = 0f;
    }

    // Update is called once per frame
    void Update() {
        UpdateBalance();
        if (ducking)
            duckTimer += Time.deltaTime;
        Debug.Log(duckTimer);
        Debug.Log(ducking);
        if (duckTimer >= 2.0f && ducking)
            Unduck();
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

    public void Jump() {
        if (jumping || ducking) return;
        jumping = true;
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpDistance), ForceMode2D.Impulse);
    }

    public void Duck() {
        if (ducking || jumping) return;
        ducking = true;
        Vector2 playerHeight = transform.localScale;
        playerHeight.y = playerHeight.y * duckScale;
        transform.localScale = playerHeight;
    }

    private void Unduck() {
        if (!ducking) return;
        Vector2 playerheight = transform.localScale;
        playerheight.y = playerheight.y / duckScale;
        transform.localScale = playerheight;
        ducking = false;
        duckTimer = 0.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Highwire") {
            jumping = false;
        }

        if (collision.gameObject.name == "Bird Left") {
            Debug.Log("Game Over");
        }
        else if (collision.gameObject.name == "Bird Right") {
            Debug.Log("Game Over");
        }
        else if (collision.gameObject.name == "Squirrel") {
            Debug.Log("Game Over");
        }
    }
}
