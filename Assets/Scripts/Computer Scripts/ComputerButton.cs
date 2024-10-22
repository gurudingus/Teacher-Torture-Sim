using UnityEngine;

public class ComputerButton : MonoBehaviour
{
    //setup computer variables

    public CameraScript cameraScript;
    public GameObject door;

    public void PanelManage(GameObject panel)
    {
        //deactivate/activate panels on request
        bool on = panel.activeInHierarchy;
        panel.SetActive(!on);
    }

    public void quit()
    {
        //exits the computer state
        cameraScript.ComputerInteraction();
    }

    public void openDoor()
    {
        //destroys door game object
        Destroy(door);
    }

    public void installVirus()
    {
        //initiates virus ending
        cameraScript.ComputerInteraction();
        CutsceneManager.PlayCutscene(GameEvent.Ending1);
    }
}
