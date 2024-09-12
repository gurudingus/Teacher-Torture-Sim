using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerButton : MonoBehaviour
{
    public void PanelManage(GameObject panel)
    {
        bool on = panel.activeInHierarchy;
        panel.SetActive(!on);
    }
}
