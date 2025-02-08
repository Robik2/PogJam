using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscenes : MonoBehaviour {
    private Animator anim;
    [SerializeField] private string skipSceneName;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            SceneManager.LoadScene(skipSceneName);
        }
    }
}
