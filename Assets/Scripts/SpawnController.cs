using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public GameObject squirrel;
    public GameObject leftBird;
    public GameObject rightBird;

    private float spawnTimeDelta;
    private int enemySelection;
    private const int SQUIRREL = 0;
    private const int LEFT_BIRD = 1;
    private const int RIGHT_BIRD = 2;

    // Use this for initialization
    void Start () {
        spawnTimeDelta = Random.Range(1.0f, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimeDelta -= Time.deltaTime;

        if (spawnTimeDelta <= 0) {
            enemySelection = Random.Range(0, 3);
            switch(enemySelection) {
                case 0:
                    spawnSquirrel();
                    break;
                case 1:
                case 2:
                    spawnBird(enemySelection);
                    break;
                default:
                    Debug.Log("Enemy could not be selected");
                    break;
            }
            spawnTimeDelta = Random.Range(1.0f, 5.0f);
        }
    }

    private void spawnSquirrel() {
        Instantiate(squirrel);
    }

    private void spawnBird(int selection) {
        if(selection == LEFT_BIRD) {
            Instantiate(leftBird);
        }
        else if (selection == RIGHT_BIRD) {
            Instantiate(rightBird);
        }
        else {
            Debug.Log("Invalid enemy selection");
        }
    }
}
