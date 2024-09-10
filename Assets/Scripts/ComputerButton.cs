using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerButton : MonoBehaviour
{
    public GameObject BrowserPanel;
    public GameObject DesktopPanel;
    public GameObject VirusPanel;
    public GameObject AdminPanel;
    public int ComputerState;

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(false);
    }

}
