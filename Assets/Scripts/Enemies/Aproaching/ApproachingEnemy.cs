using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ApproachingEnemy : MonoBehaviour {
    private Transform target;
    private bool hasHit;
    [HideInInspector] public bool isHit;
    [SerializeField] private NavMeshAgent agent;
    [HideInInspector] public Vector3 knockupPos;
    [SerializeField] private float hitCD;
    [SerializeField] private float swimSpeed;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioClip swimClip;

    private float lastSwimClip;
    
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
        } else if (hasHit) {
            agent.speed = 0;
        } else { 
            if (Time.time - lastSwimClip > swimClip.length) {
                lastSwimClip = Time.time;
                SoundManager.instance.PlaySoundClip(swimClip, transform, .35f);
            }
            agent.speed = swimSpeed;
            agent.SetDestination(target.position);
        }

        if (anim != null) {
            anim.SetBool("hit", isHit);
            anim.SetFloat("speed", agent.velocity.magnitude);
        }
        
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        if (isHit) { return; }
        RotateTowardsPlayer();        
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
        if(anim != null)
            anim.SetBool("chomp", true);

        yield return new WaitForSeconds(cd);

        if(anim != null)
            anim.SetBool("chomp", false);
        hasHit = false;
    }
    
    private void RotateTowardsPlayer() {
        Vector3 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation;
        if (transform.position.x > target.position.x) {
            rotation = Quaternion.Euler(0, 0, angle);
            transform.localScale = new Vector3(1, 1, 1);
        } else {
            rotation = Quaternion.Euler(0, 0, angle - 180);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 120);
    }
}
