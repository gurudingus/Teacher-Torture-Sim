using UnityEngine;


public enum CameraState
{
    Player,
    Computer
}

public class CameraScript : MonoBehaviour
{
    //setup camera variables and defenitions
    private float rotateCameraPitch;

    private Camera firstPersonCam;

    public float mouseSensitivity = 2f;

    public float pitchRange = 60f;

    private CharacterController characterController;
    private MeshRenderer meshRenderer;

    public PositionRotation computerCamera { get; set; }
    private Vector3 cameraPosition;

    public CameraState camState = CameraState.Player;

    private void Awake()
    {
        //ensures the camera is set to the correct object
        firstPersonCam = GetComponentInChildren<Camera>();
        cameraPosition = firstPersonCam.transform.localPosition;

        characterController = transform.parent.GetComponent<CharacterController>();
        meshRenderer = transform.parent.GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        //locks cursor to screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MoveToPos(camState == CameraState.Computer);

        if (camState == CameraState.Player)
        {
            //rotate player around
            float rotateYaw = Input.GetAxis("Mouse X") * mouseSensitivity * Time.timeScale;
            transform.parent.transform.Rotate(0, rotateYaw, 0);

            //rotate cam up and down
            rotateCameraPitch += -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.timeScale;
            //lock rotation so no flip camera
            rotateCameraPitch = Mathf.Clamp(rotateCameraPitch, -pitchRange, pitchRange);
            firstPersonCam.transform.localRotation = Quaternion.Euler(rotateCameraPitch, 0, 0);
        }
    }

    public void ComputerInteraction()
    {
        camState = camState == CameraState.Player ? CameraState.Computer : CameraState.Player; //Swap the camera state

        bool isPlayer = camState == CameraState.Player;
        GameManager.gameState = isPlayer ? GameState.Playing : GameState.Computer;

        Cursor.lockState = isPlayer ? CursorLockMode.Locked : CursorLockMode.None;
        characterController.enabled = isPlayer;
        meshRenderer.enabled = isPlayer;
    }

    //sets camera to the correct 
    public void MoveToPos(bool position)
    {
        if (!position)
        {
            transform.position = Vector3.Lerp(transform.position, (Vector3)(transform.parent.localToWorldMatrix * cameraPosition) + transform.parent.position, Time.deltaTime * 10);
        }
        else if (position)
        {
            transform.position = Vector3.Lerp(transform.position, computerCamera.Position, Time.deltaTime * 10); //Second transform needs to be the transform of the computer
            transform.rotation = computerCamera.Rotation;
        }
    }
}