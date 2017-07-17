using UnityEngine;

public class MrStuntman : MonoBehaviour {

    public float rebalanceRate = 1f;
    public float jumpDistance = 0.3f;
    public float duckScale = 0.4f;
    public float duckTimer = 0f;
    public float rotationMultiplier = 2f;
    public bool hasFallen = false;
    public AudioSource stepAudio;
    public AudioSource hitAudio;
    public AudioSource jumpAudio;

    private int balanceAngle;
    private bool ducking = false;
    private bool jumping = false;

    private float rebalanceTimeDetla;

    // Use this for initialization
    void Start() {
        rebalanceTimeDetla = 0f;
    }

    // Update is called once per frame
    void Update() {
        Rebalance();
        balanceAngle = Mathf.RoundToInt(transform.rotation.eulerAngles.z);
        // Check the balance angle to see if Mr Stuntman will fall off the Highwire
        if (10 < balanceAngle && balanceAngle < 350) {
            hasFallen = true;
        }
        else {

            if (ducking) {
                duckTimer += Time.deltaTime;
            }
            if (duckTimer >= 1.0f && ducking) {
                Unduck();
            }
        }
    }

    public void PositiveRotation(Vector3 rotation) {
        transform.Rotate(rotation);
        rebalanceTimeDetla = 0f;
    }

    public void PositiveRotation() {
        PositiveRotation(Vector3.forward * rotationMultiplier);
    }

    public void NegativeRotation(Vector3 rotation) {
        transform.Rotate(rotation);
        rebalanceTimeDetla = 0f;
    }

    public void NegativeRotation() {
        NegativeRotation(Vector3.back * rotationMultiplier);
    }

    public void Jump() {
        if (jumping || ducking) return;
        jumping = true;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpDistance), ForceMode2D.Impulse);
        stepAudio.mute = true;
        jumpAudio.Play();
    }

    public void Duck() {
        if (ducking || jumping) return;
        ducking = true;
        Vector2 playerHeight = transform.localScale;
        playerHeight.y = playerHeight.y * duckScale;
        transform.localScale = playerHeight;
    }

    public void PlayStepAudio() {
        stepAudio.Play();
    }

    private void Rebalance() {
        balanceAngle = Mathf.RoundToInt(transform.rotation.eulerAngles.z);

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

    private void Unduck() {
        if (!ducking) return;
        Vector2 playerheight = transform.localScale;
        playerheight.y = playerheight.y / duckScale;
        transform.localScale = playerheight;
        ducking = false;
        duckTimer = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name.Equals("Highwire")) {
            jumping = false;
            stepAudio.mute = false;
        }
    }
}
