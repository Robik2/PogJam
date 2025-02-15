using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    
    public bool IsPaused { get; set; }
    public int deepLevel;
    
    public bool alreadyStarted;
    public float playerHealth;

    public void Pause() {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        IsPaused = Time.timeScale == 0;
        PauseCanvas.instance.GetComponent<Canvas>().enabled = IsPaused;
    }
}
