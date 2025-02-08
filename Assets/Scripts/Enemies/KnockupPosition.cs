using UnityEngine;

public class KnockupPosition : MonoBehaviour {
    [SerializeField] private Transform follow;

    private void Start() {
        transform.SetParent(null);
    }

    private void Update() {
        transform.position = new Vector3(0, 10, 0);
    }
}
