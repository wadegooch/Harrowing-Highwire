using UnityEngine;

public class MrStuntman : MonoBehaviour {

    public float rebalanceRate = 1f;
    public float jumpDistance = 0.3f;
    public float duckScale = 0.4f;
    public float duckTimer = 0.0f;
    public bool ducking = false;
    public bool jumping = false;

    private float rebalanceTimeDetla;

    // Use this for initialization
    void Start() {
        rebalanceTimeDetla = 0f;
    }

    // Update is called once per frame
    void Update() {
        UpdateBalance();
        if (ducking) {
            duckTimer += Time.deltaTime;
        }
        if (duckTimer >= 1.0f && ducking) {
            Unduck();
        }
    }

    public void PositiveRotation() {
        transform.Rotate(Vector3.forward * 2);
        rebalanceTimeDetla = 0f;
    }

    public void NegativeRotation() {
        transform.Rotate(Vector3.back * 2);
        rebalanceTimeDetla = 0f;
    }

    void UpdateBalance() {
        int balanceAngle = Mathf.RoundToInt(transform.rotation.eulerAngles.z);

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
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpDistance), ForceMode2D.Impulse);
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

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name.Equals("Highwire")) {
            jumping = false;
        }
    }
}
