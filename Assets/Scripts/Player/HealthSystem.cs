
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour {
    public static HealthSystem instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    
    [SerializeField] private float maxHealth;
    [SerializeField] private float invincibilityTime;
    [SerializeField] private Animator anim;
    private float currentHealth;
    private bool hasBeenHit;

    private Renderer rend;
    
    private void Start() {
        rend = GetComponentInChildren<Renderer>();
        currentHealth = GameManager.instance.alreadyStarted == false ? maxHealth : GameManager.instance.playerHealth;
    }
    
    public void TakeDamage() {
        if (hasBeenHit) { return; }

        currentHealth--;

        if (currentHealth <= 0) {
            Death();
            return;
        }
        
        StartCoroutine(IFrames(invincibilityTime));
    }

    private void Death() {
        Destroy(GameManager.instance);
        SceneManager.LoadScene("Menu");
    }
    
    private IEnumerator IFrames(float time) {
        hasBeenHit = true;
        anim.SetBool("hit", true);
        
        yield return new WaitForSeconds(time);

        anim.SetBool("hit", false);
        hasBeenHit = false;
    }
}
