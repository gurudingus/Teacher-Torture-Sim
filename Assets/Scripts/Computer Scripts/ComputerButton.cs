using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerButton : MonoBehaviour
{

    public CameraScript cameraScript;
    public void PanelManage(GameObject panel)
    {
        bool on = panel.activeInHierarchy;
        panel.SetActive(!on);
    }

    public void quit()
    {
        cameraScript.moveToPos(false);
    }
}