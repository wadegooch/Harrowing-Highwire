using UnityEngine;

public class SquirrelController : MonoBehaviour {

    public float runSpeed = -0.6f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0f, runSpeed) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision is BoxCollider2D && collision && collision.attachedRigidbody.velocity == new Vector2(0.0f, 0.0f)) {
            Debug.Log("Game Over");
        }
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
