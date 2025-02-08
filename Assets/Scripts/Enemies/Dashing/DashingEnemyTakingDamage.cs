using System.Collections;
using UnityEngine;

public class DashingEnemyTakingDamage : MonoBehaviour {
    [SerializeField] private DashingEnemy enemy;
    [SerializeField] private float knockupDuration;
    [SerializeField] private GameObject bubble;
    [SerializeField] private Collider2D col;
    private bool alreadyHit;
    
    public void Hit(Vector3 pos) {
        if (alreadyHit) { return; }

        enemy.knockupPos = pos + Vector3.up * 2;
            
        StartCoroutine(HitEnum());
    }

    private IEnumerator HitEnum() {
        enemy.isHit = true;
        alreadyHit = true;
        bubble.SetActive(true);
        col.enabled = false;
                
        yield return new WaitForSeconds(knockupDuration);

        col.enabled = true;
        bubble.SetActive(false);
        alreadyHit = false;
        enemy.isHit = false;
    }
}