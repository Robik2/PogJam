using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rybk : MonoBehaviour
{
    [SerializeField] private List<Transform> target;
    private int currentTarget;
    [SerializeField] private NavMeshAgent agent;
    
    private void Start() {
        currentTarget = 0;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        foreach (Transform t in target) {
            t.SetParent(null);
        }
    }

    private void Update() {
        if(GameManager.instance.IsPaused) { return; }

        agent.SetDestination(target[currentTarget].position);

        if (Vector3.Distance(transform.position, target[currentTarget].position) < .5f) {
            currentTarget++;
            if (currentTarget == target.Count) {
                currentTarget = 0;
            }
        }
        
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        RotateTowardsPlayer();        
    }
    
    private void RotateTowardsPlayer() {
        Vector3 direction = (target[currentTarget].position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation;
        if (transform.position.x > target[currentTarget].position.x) {
            rotation = Quaternion.Euler(0, 0, angle);
            transform.localScale = new Vector3(1, 1, 1);
        } else {
            rotation = Quaternion.Euler(0, 0, angle - 180);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 120);
    }
}
