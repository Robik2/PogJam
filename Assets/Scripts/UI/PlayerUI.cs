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
        
        boostBubble.color = new Color(.48f, .48f, .48f);
    }
    
    [SerializeField] private List<GameObject> hearts;
    [SerializeField] private Image boostBubble;

    public void UpdateHearts() {
        hearts[(int)HealthSystem.instance.currentHealth].GetComponent<Image>().color = new Color(0.54f, 0, 0);
    }

    public void UpdateBoost(bool active) {
        boostBubble.color = active ? Color.white : new Color(.48f, .48f, .48f);
    }
}
