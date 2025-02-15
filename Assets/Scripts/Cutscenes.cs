using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscenes : MonoBehaviour {
    private Animator anim;
    [SerializeField] private string skipSceneName;
    [SerializeField] private bool lastCutscene;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            if (lastCutscene) {
                if (GameObject.Find("Myzuka Gry") != null)
                    Destroy(GameObject.Find("Myzuka Gry"));
        
                if(GameManager.instance != null)
                    Destroy(GameManager.instance.gameObject);
        
                if(PauseCanvas.instance != null)
                    Destroy(PauseCanvas.instance.gameObject);
        
                if(PlayerUI.instance != null)
                    Destroy(PlayerUI.instance.gameObject);
            }
            
            SceneManager.LoadScene(skipSceneName);
        }
    }
}
