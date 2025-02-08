using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movement Stats")] 
    [SerializeField] private float swimSpeed;
    [SerializeField] private float speedWhenRotating;
    [SerializeField] private float swimCooldown;
    [SerializeField] private float rotationSpeed;

    private float x, y, previousHorizontalInput, lastSwimTime;
    private Quaternion rotateTo;

    [Header("Transforms etc.")] 
    [SerializeField] private Rigidbody2D rb;

    private void Update() {
        GetRotationInput();
            
        Swim();

        if(Input.GetButton("Dash") && Time.time - lastSwimTime > swimCooldown) {Dash();}
    }

    private void FixedUpdate() {
        RotatePlayer();
    }

    private void GetRotationInput() {
        if (Input.GetKeyDown(KeyCode.D)) x = 1f;
        if (Input.GetKeyDown(KeyCode.A)) x = -1f;
        
        if (Input.GetKeyUp(KeyCode.D)) x = (Input.GetKey(KeyCode.A)) ? -1f : 0f;
        if (Input.GetKeyUp(KeyCode.A)) x = (Input.GetKey(KeyCode.D)) ? 1f : 0f; 
    }

    private void Swim() {
        if (rb.velocity.magnitude < speedWhenRotating) {
            rb.velocity = Input.GetAxisRaw("Vertical") switch {
                1 => transform.up * speedWhenRotating,
                0 => Vector3.zero,
                -1 => transform.up * -speedWhenRotating / 2,
                _ => Vector3.zero
            };
        }
    }
    
    private void RotatePlayer() {
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTo, rotationSpeed * Time.fixedDeltaTime);
        transform.Rotate(x * rotationSpeed * Time.deltaTime, 0, 0);
        if (Input.GetAxisRaw("Vertical") == 1 && rb.velocity.magnitude < speedWhenRotating) {
            rb.velocity = transform.up * speedWhenRotating;
        }
    }

    private void Dash() {
        lastSwimTime = Time.time;
        // rb.velocity = transform.up * swimSpeed;
        rb.velocity = transform.up * swimSpeed;
    }
}
