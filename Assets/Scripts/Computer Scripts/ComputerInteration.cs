using UnityEngine;

public class ComputerInteration : MonoBehaviour, IInteractable
{
    public CameraScript cameraScript;
    public void Interact(InteractionScript source) => cameraScript.ComputerInteraction(source);
}
