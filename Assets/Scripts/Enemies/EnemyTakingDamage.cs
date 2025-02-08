using System.Collections;
using UnityEngine;

public class EnemyTakingDamage : MonoBehaviour {
    [SerializeField] private ApproachingEnemy enemy;
    [SerializeField] private float knockupDuration;
    private bool alreadyHit;
    
    public void Hit(Vector3 pos) {
        if (alreadyHit) { return; }

        enemy.knockupPos = pos + Vector3.up * 10;
            
        StartCoroutine(HitEnum());
    }

    private IEnumerator HitEnum() {
        enemy.isHit = true;
        alreadyHit = true;
                
        yield return new WaitForSeconds(knockupDuration);

        alreadyHit = false;
        enemy.isHit = false;
    }
}
