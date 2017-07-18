using UnityEngine;

public class BirdController : MonoBehaviour {

    public float flightSpeedX;

    private float flightSpeedY = -1.15f;
    private MrStuntman mrStuntman;

    // Use this for initialization
    void Start() {
        mrStuntman = FindObjectOfType<MrStuntman>();
    }

    // Update is called once per frame
    void Update() {
        transform.position += new Vector3(flightSpeedX, flightSpeedY) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision is CircleCollider2D) {
            mrStuntman.hitAudio.Play();
            mrStuntman.Fall();
        }
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
