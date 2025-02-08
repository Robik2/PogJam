
using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    [SerializeField] private float maxHealth;
    [SerializeField] private float invincibilityTime;
    private float currentHealth;
    private bool hasBeenHit;

    private Material modelMaterial;
    private Renderer rend;
    
    private void Start() {
        rend = GetComponentInChildren<Renderer>();
        modelMaterial = new Material(rend.material);
        rend.material = modelMaterial;
    }
    
    public void TakeDamage() {
        if (hasBeenHit) { return; }
        StartCoroutine(IFrames(invincibilityTime));
    }

    private IEnumerator IFrames(float time) {
        hasBeenHit = true;
        modelMaterial.color = Color.red;
        
        yield return new WaitForSeconds(time);

        hasBeenHit = false;
        modelMaterial.color = Color.white;
    }
}
