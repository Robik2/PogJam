using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private Image boostIcon;

    private Canvas canv;

    private void Start() {
        canv = GetComponent<Canvas>();
    }

    private void Update() {
        canv.enabled = HealthSystem.instance != null;
    }
    
    public void UpdateHearts() {
        if(HealthSystem.instance.currentHealth > 0)
            healthIcon.sprite = tank[(int)HealthSystem.instance.currentHealth - 1];
    }

    public void UpdateBoost(bool geyserBoost) {
        boostIcon.color = geyserBoost ? Color.white : new Color(.53f, .53f, .53f);
    }
}
