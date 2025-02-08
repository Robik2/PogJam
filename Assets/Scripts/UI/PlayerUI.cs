using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public static PlayerUI instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    [SerializeField] private Image healthIcon;
    [SerializeField] private List<Sprite> tank;

    public void UpdateHearts() {
        if(HealthSystem.instance.currentHealth > 0)
            healthIcon.sprite = tank[(int)HealthSystem.instance.currentHealth - 1];
    }
}
