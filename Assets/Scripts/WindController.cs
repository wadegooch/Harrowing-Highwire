using UnityEngine;

public class WindController : MonoBehaviour {

    public MrStuntman mrStuntman;

    private float persistTimeDelta = 3f;
    private float rotationMultiplier = 0.1f;
    private bool isActive = false;

    // Use this for initialization
    void Start() {
        mrStuntman = FindObjectOfType<MrStuntman>();
    }

    // Update is called once per frame
    void Update() {
        if (isActive) {
            persistTimeDelta -= Time.deltaTime;
            if (persistTimeDelta <= 0f) {
                Destroy(gameObject);
                isActive = false;
                persistTimeDelta = 3f;
            }
            // Handle Mr Stuntman rotation for wind direction
            if (gameObject.name.Contains("Left")) {
                mrStuntman.NegativeRotation(Vector3.back * rotationMultiplier);
            }
            else {
                mrStuntman.PositiveRotation(Vector3.forward * rotationMultiplier);
            }
        }
    }

    public void OnBecameVisible() {
        isActive = true;
    }
}
