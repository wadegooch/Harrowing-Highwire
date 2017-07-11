using System.Collections.Generic;
using UnityEngine;

public class CloudPool : MonoBehaviour {

    public GameObject cloudsPrefab;
    public float spawnRate = 5f;
    //public float leftXPos = -5.6f;
    //public float rightXPos = 5.6f;

    private List<GameObject> clouds;
    private Vector2 cloudPoolPosition = new Vector2(-25f, -35f);
    private float leftXPos;
    private float rightXPos;
    private float yPos;
    private float spawnTimeDelta;
    private int currentCloud = 0;

	// Use this for initialization
	void Start () {
        spawnTimeDelta = 0f;
        leftXPos = Camera.main.ViewportToWorldPoint(new Vector3(0.075f, 1)).x;
        rightXPos = Camera.main.ViewportToWorldPoint(new Vector3(0.925f, 1)).x;
        yPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 1)).y;
        clouds = new List<GameObject>();

        foreach (Transform cloud in cloudsPrefab.transform) {
            clouds.Add(Instantiate(cloud.gameObject, cloudPoolPosition, Quaternion.identity));
        }
        spawnTimeDelta = 5f;
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimeDelta += Time.deltaTime;

        if (spawnTimeDelta >= spawnRate) {
            spawnTimeDelta = 0f;
            
            // Spawn clouds on left and right evenly
            if ((currentCloud % 2) != 0) {
                clouds[currentCloud].transform.position = new Vector2(leftXPos, yPos);
            }
            else {
                clouds[currentCloud].transform.position = new Vector2(rightXPos, yPos);
            }

            currentCloud++;

            if (currentCloud >= clouds.Count) {
                currentCloud = 0;
            }
        }
	}
}
