using UnityEngine;

public class ScrollPositionUpdate : MonoBehaviour {
    private void Update() {
        transform.parent.position = transform.position;
        transform.parent.rotation = transform.rotation;

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
