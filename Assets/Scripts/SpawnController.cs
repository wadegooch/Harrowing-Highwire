using UnityEngine;

public class SpawnController : MonoBehaviour {

    public GameObject squirrel;
    public GameObject leftBird;
    public GameObject rightBird;
    public GameObject leftWind;
    public GameObject rightWind;
    public float spawnTimeMin = 1.25f;
    public float spawnTimeMax = 4f;

    private float spawnTimeDelta;
    private int hazardSelection;
    private const int SQUIRREL = 0;
    private const int LEFT_BIRD = 1;
    private const int RIGHT_BIRD = 2;
    private const int LEFT_WIND = 3;
    private const int RIGHT_WIND = 4;

    // Use this for initialization
    void Start () {
        spawnTimeDelta = Random.Range(spawnTimeMin, spawnTimeMax);
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimeDelta -= Time.deltaTime;

        if (spawnTimeDelta <= 0) {
            hazardSelection = Random.Range(0, 5);
            switch(hazardSelection) {
                case SQUIRREL:
                    SpawnSquirrel();
                    break;
                case LEFT_BIRD:
                case RIGHT_BIRD:
                    SpawnBird(hazardSelection);
                    break;
                case LEFT_WIND:
                case RIGHT_WIND:
                    SpawnWind(hazardSelection);
                    break;
                default:
                    Debug.Log("Enemy could not be selected");
                    break;
            }
            spawnTimeDelta = Random.Range(spawnTimeMin, spawnTimeMax);
        }
    }

    private void SpawnSquirrel() {
        Instantiate(squirrel);
    }

    private void SpawnBird(int selection) {
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

    private void SpawnWind(int selection) {
        if (selection == LEFT_WIND) {
            Instantiate(leftWind);
        }
        else if (selection == RIGHT_WIND) {
            Instantiate(rightWind);
        }
        else {
            Debug.Log("Invalid enemy selection");
        }
    }
}
