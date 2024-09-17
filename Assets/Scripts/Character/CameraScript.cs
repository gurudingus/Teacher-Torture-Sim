using UnityEngine;


public enum CameraState
{
    Player,
    Computer
}

public class CameraScript : MonoBehaviour
{
    private float rotateCameraPitch;

    private Camera firstPersonCam;

    public float mouseSensitivity = 2f;

    public float pitchRange = 60f;

    public GameObject player;

    //#region added by Liam
    private CharacterController characterController;
    private MeshRenderer meshRenderer;
    //#endregion added by Liam

    public Transform camPos;
    public Transform playerPos;

    public CameraState camState = CameraState.Player;

    private void Awake()
    {
        firstPersonCam = GetComponentInChildren<Camera>();

        //#region added by Liam
        characterController = player.GetComponent<CharacterController>();
        meshRenderer = player.GetComponent<MeshRenderer>();
        //#endregion added by Liam
    }

    private void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //cam state 0 is for the player camera
        if (camState == CameraState.Player)
        {
            moveToPos(false);
            //rotate player around
            float rotateYaw = Input.GetAxis("Mouse X") * mouseSensitivity;
            player.transform.Rotate(0, rotateYaw, 0);

            //rotate cam up and down
            rotateCameraPitch += -Input.GetAxis("Mouse Y") * mouseSensitivity;
            //lock rotation so no flip camera
            rotateCameraPitch = Mathf.Clamp(rotateCameraPitch, -pitchRange, pitchRange);
            firstPersonCam.transform.localRotation = Quaternion.Euler(rotateCameraPitch, 0, 0);
        }
        else if (camState == CameraState.Computer)
        {
            bool isPlayer = camState == CameraState.Player;

            moveToPos(!isPlayer);
        }
        
    }

    public void ComputerInteraction() {
        camState = camState == CameraState.Player ? CameraState.Computer : CameraState.Player;

        bool isPlayer = camState == CameraState.Player;

        Cursor.lockState = isPlayer ? CursorLockMode.Locked : CursorLockMode.None;
        characterController.enabled = isPlayer;
        meshRenderer.enabled = isPlayer;
    }

    //sets camera to the correct 
    public void moveToPos(bool position)
    {
        if (position == false)
        {
            transform.position = Vector3.Lerp(transform.position, playerPos.position, Time.deltaTime * 10);
        }
        else if (position == true)
        {
            transform.position = Vector3.Lerp(transform.position, camPos.position, Time.deltaTime * 10);
            transform.rotation = Quaternion.Euler(0f,0f,0f);
        }
    }
}
