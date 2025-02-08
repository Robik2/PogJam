using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {
    [SerializeField] private string sceneName;
    [SerializeField] private bool isReverse;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            GameManager.instance.alreadyStarted = true;
            GameManager.instance.playerHealth = HealthSystem.instance.currentHealth;
            GameManager.instance.deepLevel += isReverse ? -1 : 1;
            SceneManager.LoadScene(sceneName);
        }
    }
}
