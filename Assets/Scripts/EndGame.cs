using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) { SceneManager.LoadScene(HealthSystem.instance.currentHealth == 3 ? "EndGood" : "EndBad"); }
    }
}
