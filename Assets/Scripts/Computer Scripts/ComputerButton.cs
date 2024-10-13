using UnityEngine;

public class ComputerButton : MonoBehaviour
{

    public CameraScript cameraScript;
    public GameObject door;

    public void PanelManage(GameObject panel)
    {
        bool on = panel.activeInHierarchy;
        panel.SetActive(!on);
    }

    public void quit()
    {
        cameraScript.ComputerInteraction();
    }

    public void openDoor()
    {
        Destroy(door);
    }

    public void installVirus()
    {
        cameraScript.ComputerInteraction();
        CutsceneManager.PlayCutscene(GameEvent.Ending1);
    }
}
