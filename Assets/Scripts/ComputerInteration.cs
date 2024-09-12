using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteration : MonoBehaviour, IInteractable
{
    public CameraScript cameraScript;
    public void Interact()
    {
        cameraScript.moveToPos(false);
    }
}
