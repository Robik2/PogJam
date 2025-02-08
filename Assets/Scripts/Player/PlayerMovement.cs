using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movement Stats")] 
    [SerializeField] private float swimSpeed;
    [SerializeField] private float speedLossPerLevel;
    [SerializeField] private float speedWhenRotating;
    [SerializeField] private float swimCooldown;
    [SerializeField] private float rotationSpeed;

    [HideInInspector] public bool geyserBoost;

    private float x, y, previousHorizontalInput, lastSwimTime;
    private Quaternion rotateTo;

    [Header("Transforms etc.")] 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioClip swimClip;
    [SerializeField] private AudioClip dashClip;

    private AudioSource swimSource;
    private float lastSwimClip, lastDashClip;
    
    private void Update() {
        if (Input.GetButtonDown("Cancel")) {
            GameManager.instance.Pause();
        }

        if(GameManager.instance.IsPaused) { return; }
        
        GetRotationInput();
            
        Swim();

        if(Input.GetButton("Dash") && Time.time - lastSwimTime > swimCooldown) {Dash();}
    }

    private void FixedUpdate() {
        if(GameManager.instance.IsPaused) { return; }
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


        if (Input.GetAxisRaw("Vertical") != 0 && Time.time - lastSwimClip > swimClip.length) {
            lastSwimClip = Time.time;
            SoundManager.instance.PlaySoundClip(swimClip, transform, 1);
        }
    }
    
    private void RotatePlayer() {
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTo, rotationSpeed * Time.fixedDeltaTime);
        transform.Rotate(0, 0, -x * rotationSpeed * Time.deltaTime);
        if (Input.GetAxisRaw("Vertical") == 1 && rb.velocity.magnitude < speedWhenRotating) {
            rb.velocity = transform.up * speedWhenRotating;
        }
    }

    private void Dash() {
        lastSwimTime = Time.time;
        // rb.velocity = transform.up * swimSpeed;
        SoundManager.instance.PlaySoundClip(dashClip, transform, .1f);
        print(geyserBoost);
        rb.velocity = transform.up * ((swimSpeed - speedLossPerLevel * GameManager.instance.deepLevel) + (swimSpeed - speedLossPerLevel * GameManager.instance.deepLevel) * Convert.ToInt32(geyserBoost) / 2);
        PlayerUI.instance.UpdateBoost(false);
        geyserBoost = false;
    }
}
