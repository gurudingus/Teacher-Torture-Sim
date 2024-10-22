using UnityEngine;

public class ScrollPositionUpdate : MonoBehaviour {
    private void FixedUpdate() {
        //Liam Script
        //updates the position of the scroll to the desired location.
        transform.parent.position = transform.position;
        transform.parent.rotation = transform.rotation;

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
