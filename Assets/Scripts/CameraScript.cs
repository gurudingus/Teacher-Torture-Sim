using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float rotateCameraPitch;

    private Camera firstPersonCam;

    public float mouseSensitivity = 2f;

    public float pitchRange = 60f;

    private void Awake()
    {
        firstPersonCam = GetComponentInChildren<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
            //rotate player around
            float rotateYaw = Input.GetAxis("Mouse X") * mouseSensitivity;
            transform.Rotate(0, rotateYaw, 0);

            //rotate cam up and down
            rotateCameraPitch += -Input.GetAxis("Mouse Y") * mouseSensitivity;
            //lock rotation so no flip camera
            rotateCameraPitch = Mathf.Clamp(rotateCameraPitch, -pitchRange, pitchRange);
            firstPersonCam.transform.localRotation = Quaternion.Euler(rotateCameraPitch, 0, 0);
    }
}
