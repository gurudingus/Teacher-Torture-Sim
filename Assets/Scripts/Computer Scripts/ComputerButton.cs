using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerButton : MonoBehaviour
{

    public CameraScript cameraScript;
    public GameObject door;

    public GameObject playerCamera;
    public GameObject cutsceneCamera;
    public void PanelManage(GameObject panel)
    {
        bool on = panel.activeInHierarchy;
        panel.SetActive(!on);
    }

    public void quit()
    {
        cameraScript.ComputerInteraction(null);
    }

    public void openDoor()
    {
        Destroy(door);
    }

    public void installVirus()
    {
        playerCamera.SetActive(false);
        cutsceneCamera.SetActive(true);

        cameraScript.ComputerInteraction(null);
        GameManager.gameState = GameState.Cutscene;
    }
}
