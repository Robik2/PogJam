using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
        if (GameManager.instance != null) {
            GameManager.instance.IsPaused = false;
        }
    }

    public void ExitButton() {
        Application.Quit();
    }

    public void DestroyOnLoad() {
        if (GameObject.Find("Myzuka Gry") != null)
            Destroy(GameObject.Find("Myzuka Gry"));
        
        if(GameManager.instance != null)
            Destroy(GameManager.instance.gameObject);
        
        if(PauseCanvas.instance != null)
            Destroy(PauseCanvas.instance.gameObject);
        
        if(PlayerUI.instance != null)
            Destroy(PlayerUI.instance.gameObject);
    }
}
