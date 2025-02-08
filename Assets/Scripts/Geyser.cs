using UnityEngine;

public class Geyser : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerUI.instance.UpdateBoost(true);
            other.GetComponent<PlayerMovement>().geyserBoost = true;
        }
    }
}
