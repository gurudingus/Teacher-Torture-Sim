using UnityEngine;

public class ComputerInteration : MonoBehaviour, IInteractable
{
    public CameraScript cameraScript;
    public void Interact(InteractionOnKey source) => cameraScript.ComputerInteraction(source);
}
