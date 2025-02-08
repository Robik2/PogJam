using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    private void Awake() {
        if (instance == null) {
            instance = this;   
        } else {
            Destroy(gameObject);
        }
    }

    [SerializeField] private Canvas pauseCanvas;
    
    public bool IsPaused { get; set; }

    public void Pause() {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        IsPaused = Time.timeScale == 0;
        pauseCanvas.enabled = IsPaused;
    }
}
