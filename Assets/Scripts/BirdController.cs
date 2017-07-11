using UnityEngine;

public class BirdController : MonoBehaviour {

    public float flightSpeedX;

    private float flightSpeedY = -1.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(flightSpeedX, flightSpeedY) * Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision is CircleCollider2D) {
            Debug.Log("Game Over!");
        }
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
