using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;
    [SerializeField] private AudioSource soundFXObject;
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void PlaySoundClip(AudioClip audioClip, Transform spawnTransform, float volume) {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform);

        audioSource.clip = audioClip;

        audioSource.volume = volume;
        
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        
        Destroy(audioSource.gameObject, clipLength);
    }
}
