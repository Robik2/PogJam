using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DashingEnemy : MonoBehaviour {
    private Transform target;
    private bool hasHit;
    [HideInInspector] public bool isHit;
    [SerializeField] private NavMeshAgent agent;
    [HideInInspector] public Vector3 knockupPos;
    [SerializeField] private float hitCD;
    [SerializeField] private float dashCD;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Animator anim;
    [SerializeField] private AnimationClip clip;

    private float lastDash;
    private Quaternion rotation;
    
    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update() {
        if(GameManager.instance.IsPaused) { return; }
        
        if (isHit) {
            agent.SetDestination(knockupPos);
            agent.speed = 2;
        } else if (hasHit) { // ANIMATION FOR HIT COOLDOWN LATER DONT MAKE SWITCH
            agent.speed = 0;
        } else {
            SetSpeed();
        }
        
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        if(isHit) { return; }

        if (agent.velocity.magnitude < .5f) {
            RotateTowardsPlayer();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(GameManager.instance.IsPaused) { return; }
        
        if (other.CompareTag("Player") && hasHit == false) {
            other.GetComponent<HealthSystem>().TakeDamage();
            StartCoroutine(HitCD(hitCD));
        }
    }

    private IEnumerator HitCD(float cd) {
        hasHit = true;

        yield return new WaitForSeconds(cd);

        hasHit = false;
    }
    
    private void RotateTowardsPlayer() {
        Vector3 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rotation = Quaternion.Euler(0, 0, angle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void SetSpeed() {
        if (Time.time - lastDash < clip.length + dashCD || transform.rotation != rotation) {
            agent.speed -= speed * Time.deltaTime;
        } else {
            anim.SetTrigger("swim");
            lastDash = Time.time;
            agent.speed = speed;
            agent.SetDestination(target.position);
        }
    }
}
