using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSaveFile : MonoBehaviour
{
    private void OnMouseDown()
    {
        ResetSave();
        #if UNITY_EDITOR

        Debug.Log("RESET");

        #endif
    }

    public void ResetSave()
    {
        Events.SetEventsComplete(0);
    }
}
