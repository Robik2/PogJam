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
        Destroy(GameObject.Find("Myzuka Gry"));
        Destroy(GameManager.instance.gameObject);
        Destroy(PauseCanvas.instance.gameObject);
        Destroy(PlayerUI.instance.gameObject);
    }
}
