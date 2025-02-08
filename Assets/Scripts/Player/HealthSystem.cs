
using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    [SerializeField] private float maxHealth;
    [SerializeField] private float invincibilityTime;
    [SerializeField] private Animator anim;
    private float currentHealth;
    private bool hasBeenHit;

    private Renderer rend;
    
    private void Start() {
        rend = GetComponentInChildren<Renderer>();
    }
    
    public void TakeDamage() {
        if (hasBeenHit) { return; }
        StartCoroutine(IFrames(invincibilityTime));
    }

    private IEnumerator IFrames(float time) {
        hasBeenHit = true;
        anim.SetBool("hit", true);
        
        yield return new WaitForSeconds(time);

        anim.SetBool("hit", false);
        hasBeenHit = false;
    }
}
