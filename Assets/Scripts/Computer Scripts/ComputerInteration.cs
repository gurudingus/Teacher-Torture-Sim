using UnityEngine;

public class ComputerInteration : MonoBehaviour, IInteractable
{
    public CameraScript cameraScript;

    public PositionRotation computerCamera = new();

    private void Awake()
    {
        cameraScript.computerCamera = new(computerCamera.GetPosition(transform), computerCamera.GetRotation(transform));
    }

    private void OnDrawGizmosSelected() => PickupUtilities.DrawGizmos(computerCamera.GetPosition(transform), computerCamera.GetRotation(transform));

    public void Interact() => cameraScript.ComputerInteraction();
}
