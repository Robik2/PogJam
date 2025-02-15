using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioClip projectileClip;
    private float distance;
    private Vector3 startPosition;

    public void Shoot(float dist, float bulletSpeed) {
        distance = dist;
        startPosition = transform.position;
        rb.velocity = transform.up * bulletSpeed;
        SoundManager.instance.PlaySoundClip(projectileClip, transform, .5f);
    }

    private void Update() {
        if (Vector2.Distance(startPosition, transform.position) >= distance) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch (other.tag) {
            case "Approaching":
                other.GetComponent<ApproachingEnemyTakingDamage>().Hit(transform.position);
                Destroy(gameObject);
                break;
            
            case "Dashing":
                other.GetComponent<DashingEnemyTakingDamage>().Hit(transform.position);
                Destroy(gameObject);
                break;
            
            case "Ground":
                Destroy(gameObject);
                break;
        }
    }
}
