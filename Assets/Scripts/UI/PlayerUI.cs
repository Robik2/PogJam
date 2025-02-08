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
    
    [SerializeField] private List<GameObject> hearts;

    public void UpdateHearts() {
        print((int)HealthSystem.instance.currentHealth);
        hearts[(int)HealthSystem.instance.currentHealth].GetComponent<Image>().color = new Color(0.54f, 0, 0);
    }
}
