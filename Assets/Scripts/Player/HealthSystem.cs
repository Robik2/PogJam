
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] private AudioClip damageClip;
    [SerializeField] private AudioClip dieClip;
    [HideInInspector] public float currentHealth;
    [SerializeField] private GameObject model;
    private bool hasBeenHit;

    [HideInInspector] public bool dead;
    
    private void Start() {
        currentHealth = GameManager.instance.alreadyStarted == false ? maxHealth : GameManager.instance.playerHealth;
    }
    
    public void TakeDamage() {
        if (hasBeenHit || dead) { return; }

        currentHealth--;
        
        PlayerUI.instance.UpdateHearts();
        print(currentHealth);
        SoundManager.instance.PlaySoundClip(damageClip, transform, .03f);
        
        if (currentHealth <= 0) {
            Death();
            return;
        }
        
        StartCoroutine(IFrames(invincibilityTime));
    }

    private void Death() {
        if (dead) { return;}
        
        StartCoroutine(Die());
    }

    private IEnumerator Die() {
        dead = true;
        SoundManager.instance.PlaySoundClip(dieClip, transform, .5f);
        model.SetActive(false);

        yield return new WaitForSeconds(dieClip.length + .55f);
        
        if (GameObject.Find("Myzuka Gry") != null)
            Destroy(GameObject.Find("Myzuka Gry"));
                
        if(GameManager.instance != null)
            Destroy(GameManager.instance.gameObject);
                
        if(PauseCanvas.instance != null)
            Destroy(PauseCanvas.instance.gameObject);
                
        if(PlayerUI.instance != null)
            Destroy(PlayerUI.instance.gameObject);

        SceneManager.LoadScene("Menu");
    }
    
    private IEnumerator IFrames(float time) {
        if (dead) { yield break;}
        
        hasBeenHit = true;
        anim.SetBool("hit", true);
        
        yield return new WaitForSeconds(time);

        anim.SetBool("hit", false);
        hasBeenHit = false;
    }
}
